using AutoMapper;
using System.Linq.Dynamic;
using backend.Dto;
using backend.Interfaces;
using backend.Models;
using backend.Repositories;
using backend.Utils;
using J2N.Text;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]

public class UsersController : Controller
{
    #region Attributes

    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly SchoolContext _context;
    private readonly ITeacherRepository _teacherRepository;
    private readonly IRegistryRepository _registryRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly ITransactionRepository _transactionRepository;

    #endregion

    #region Costructor

    public UsersController(
        ITransactionRepository transactionRepository,
        IUserRepository userRepository,
        ITeacherRepository teacherRepository,
        IRegistryRepository registryRepository,
        IStudentRepository studentRepository,
        IMapper mapper,
        SchoolContext context
    )
    {
        this._userRepository = userRepository;
        this._mapper = mapper;
        _context = context;
        this._transactionRepository = transactionRepository;
        this._teacherRepository = teacherRepository;
        this._registryRepository = registryRepository;
        this._studentRepository = studentRepository;
    }

    #endregion

    #region API calls

    #region Get all users

    /// <summary> Get call on user breakpoint </summary>
    /// <returns>All User with filter by role and search</returns>
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Registry>))]
    public IActionResult GetUsers([FromQuery] PaginationParams @params)
    {
        //check if the order type is valid
        if (@params.OrderType.Trim().ToLower() != "asc" && @params.OrderType.Trim().ToLower() != "desc") 
        {
            return BadRequest($"{@params.OrderType} is not a valid order");
        }
        
        //if the role is null returns all the users
        if (@params.Role == null)
        {
            // var users = new GenericRepository<User>(_context);
            // var dummy = users.GetAll(@params, 
            //     user => user.Student != null
            //         ? user.Student.Registry.Name.Trim().ToLower().Contains(@params.Search.Trim().ToLower())
            //         : user.Teacher.Registry.Name.Trim().ToLower().Contains(@params.Search.Trim().ToLower()) ||
            //           user.Student != null
            //             ? user.Student.Registry.Surname.Trim().ToLower().Contains(@params.Search.Trim().ToLower())
            //             : user.Teacher.Registry.Surname.Trim().ToLower().Contains(@params.Search.Trim().ToLower()),
            //     user => user.Student,
            //     user => user.Student.Registry,
            //     user => user.Teacher,
            //     user => user.Teacher.Registry);

            //i start from Registry to take all information at the same time
            var registry = new GenericRepository<Registry>(_context);

            //I take all the users using the params element and its includes
            var dummyReg = registry.GetAll(@params,
                reg => reg.Name.Trim().ToLower().Contains(@params.Search.Trim().ToLower()) ||
                       reg.Surname.Trim().ToLower().Contains(@params.Search.Trim().ToLower()),
                reg => reg.Student,
                reg => reg.Teacher);
            return Ok(dummyReg);
        }

        //if the role is not null return the users which have the role equal then params.role
        switch (@params.Role.Trim().ToLower())
        {
            case "teacher":
                var teachers = new GenericRepository<Teacher>(_context);
                
                return Ok(teachers.GetAll(@params, teacher =>
                        teacher.Registry.Name.Trim().ToLower().Contains(@params.Search)
                        || teacher.Registry.Surname.Trim().ToLower()
                            .Contains(@params.Search),
                    teacher => teacher.User, teacher => teacher.Registry
                ));
            case "student":
                var students = new GenericRepository<Student>(_context);
                return Ok(students.GetAll(@params, student =>
                        student.Registry.Name.Trim().ToLower().Contains(@params.Search) //contains
                        || student.Registry.Surname.Trim().ToLower().Contains(@params.Search),
                    student => student.User, student => student.Registry //includes params
                ));
            default:
                return NotFound($"The Role \"{@params.Role}\" has not found");
        }

        return BadRequest(ModelState);
    }

    #endregion

    #region Add an UserTeacher

    /// <summary>
    /// This call is used to create an user which is a Teacher
    /// </summary>
    /// <param name="userTeacher"></param>
    /// <returns></returns>
    [HttpPost("teacher")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public IActionResult AddUserTeacher([FromBody] UserDetailDto userTeacher)
    {

        if (userTeacher == null || userTeacher.User == null || userTeacher.Registry == null)
        {
            return BadRequest(ModelState);
        }

        if (_userRepository.UserExists(userTeacher.User.Username))
        {
            ModelState.AddModelError("response", "user already exist");
            return StatusCode(422, ModelState);
        }

        // if (!ModelState.IsValid)
        // {
        //     return BadRequest(ModelState);
        // }
        ///<summary> start transaction </summary>
        var transaction = _transactionRepository.BeginTransaction();

        ///<summary> Create the user to add on db, taking the attributes from userTeacher</summary>
        var user = new User
        {
            Id = new Guid(),
            Username = userTeacher.User.Username,
            Password = userTeacher.User.Password,
        };

        ///<summary> Create the Registry to add on db, taking the attributes from userTeacher</summary>
        var registry = new Registry
        {
            Id = new Guid(),
            Name = userTeacher.Registry.Name,
            Surname = userTeacher.Registry.Surname,
            Birth = userTeacher.Registry.Birth ?? null,
            Gender = userTeacher.Registry.Gender,
            Email = userTeacher.Registry.Email ?? null,
            Address = userTeacher.Registry.Address ?? null,
            Telephone = userTeacher?.Registry.Telephone,
        };

        ///<summary> Try to create an user and registry</summary>
        if (_userRepository.CreateUser(user) && _registryRepository.CreateRegistry(registry))
        {
            var teacher = new Teacher
            {
                Id = new Guid(),
                UserId = user.Id,
                RegistryId = registry.Id,
            };

            if (this._teacherRepository.CreateTeacher(teacher))
            {
                _transactionRepository.CommitTransaction(transaction);
                return Ok("Teacher successfully created");
            }
            else
            {
                _transactionRepository.RollbackTransaction(transaction);
                return BadRequest(ModelState);
            }
        }
        else
        {
            _transactionRepository.RollbackTransaction(transaction);
            ModelState.AddModelError("Response", "Teacher is null");
            return BadRequest(ModelState);
        }
    }

    #endregion

    #region Add an UserStudent

    /// <summary>
    /// This call is used to create an user which is a Teacher
    /// </summary>
    /// <param name="userDetail"></param>
    /// <returns></returns>
    [HttpPost("student")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public IActionResult AddUserStudent([FromBody] UserDetailDto userStudent)
    {
        if (userStudent == null
            || userStudent.Classroom == null
            || userStudent.User == null ||
            userStudent.Registry == null
           )
        {
            return BadRequest(ModelState);
        }


        if (_userRepository.UserExists(userStudent.User.Username))
        {
            ModelState.AddModelError("response", "User already exist");
            return StatusCode(422, ModelState);
        }

        ///<summary> start transaction </summary>
        var transaction = _transactionRepository.BeginTransaction();

        ///<summary> Create the user to add on db, taking the attributes from userStudent</summary>
        var user = new User
        {
            Id = new Guid(),
            Username = userStudent.User.Username,
            Password = userStudent.User.Password,
        };

        ///<summary> Create the Registry to add on db, taking the attributes from userStudent</summary>
        var registry = new Registry
        {
            Id = new Guid(),
            Name = userStudent.Registry.Name,
            Surname = userStudent.Registry.Surname,
            Birth = userStudent.Registry.Birth ?? null,
            Gender = userStudent.Registry.Gender,
            Email = userStudent.Registry.Email ?? null,
            Address = userStudent.Registry.Address ?? null,
            Telephone = userStudent.Registry.Telephone ?? null,
        };

        ///<summary> Try to create an user and registry</summary>
        if (_userRepository.CreateUser(user) && _registryRepository.CreateRegistry(registry))
        {
            var student = new Student()
            {
                Id = new Guid(),
                UserId = user.Id,
                RegistryId = registry.Id,
                Classroom = userStudent.Classroom,
            };

            if (this._studentRepository.CreateStudent(student))
            {
                _transactionRepository.CommitTransaction(transaction);
                return Ok("Student successfully created");
            }
            else
            {
                _transactionRepository.RollbackTransaction(transaction);
                return BadRequest(ModelState);
            }
        }
        else
        {
            _transactionRepository.RollbackTransaction(transaction);
            ModelState.AddModelError("Response", "Student is null");
            return BadRequest(ModelState);
        }
    }

    #endregion

    #region Pdf for circular and table
    
    [HttpPost]
    [Route("pdf")]
    public IActionResult GetUsersOnPdf([FromBody] Circular? data, [FromQuery] string type = "table")
    {
        ///<summary>We return a Bytes array because the PDF is a sequence of binary bytes to represent the document content compactly. </summary>
       
        var stream = PDF.GeneratePdf(type,  _mapper.Map<List<UserDto>>(_userRepository.GetUsers()), data);
        
        // Returns the PDF
        return File(stream, "application/pdf", "generated.pdf");

    }

    #endregion

    #endregion
}