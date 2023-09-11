using System.Linq.Expressions;
using AutoMapper;
using System.Linq.Dynamic;
using backend.Dto;
using backend.Interfaces;
using backend.Models;
using backend.Repositories;
using backend.Utils;
using J2N.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        
        GenericRepository<Registry> registryRepo = new GenericRepository<Registry>(_context);
        //I take all the users using the params element and its includes
        
        ICollection<Registry> registries = registryRepo.GetAll(@params,
            reg => reg.Name.Trim().ToLower().Contains(@params.Search.Trim().ToLower()) ||
                   reg.Surname.Trim().ToLower().Contains(@params.Search.Trim().ToLower()),
            reg => reg.Student,
            reg => reg.Teacher);
        
        //if the role is null returns all the users
        if (@params.Filter == null)
        {
            return Ok(registries);
        }

        //if the role is not null return the users which have the role equal then params.role
        switch (@params.Filter.Trim().ToLower())
        {
            case "teacher":
                registries = registries.Where(reg => reg.Student == null).ToList();
                return Ok(registries);
            case "student":
                registries = registries.Where(reg => reg.Teacher == null).ToList();
                return Ok(registries);
            default:
                return NotFound($"The Role \"{@params.Filter}\" has not found");
        }
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
    public IActionResult AddUserTeacher([FromBody] AddEntity userTeacher)
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
            Telephone = userTeacher.Registry.Telephone ?? null,
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
                return Ok(teacher);
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
    public IActionResult AddUserStudent([FromBody] AddEntity userStudent)
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
                ClassroomId = userStudent.Classroom.Id,
            };
    
            if (this._studentRepository.CreateStudent(student))
            {
                _transactionRepository.CommitTransaction(transaction);
                return Ok(student);
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

    #region Delete an User

    /// <summary> Delete a using an id </summary>
    /// <param name="id">the id of an user which we delete</param>
    /// <returns>Response ok if deleted, not found if the id not exists</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public IActionResult DeleteUser(Guid id)
    {
        GenericRepository<User> users = new GenericRepository<User>(_context);
        GenericRepository<Registry> registries = new GenericRepository<Registry>(_context);
        
        var transaction = _transactionRepository.BeginTransaction();
        if (!users.Exist(user => user.Id == id))
        {
            return NotFound();
        }

        User userToDelete = users.GetById(user => user.Id == id, user => user.Student, user => user.Teacher);
        Registry registryToDelete = registries.GetById(reg => userToDelete.Student != null ? reg.Id == userToDelete.Student.RegistryId : reg.Id == userToDelete.Teacher.RegistryId);

        //When i can't delete an Entity it returns a Throw exception error then i can rollback all
        try
        {
            users.Delete(userToDelete);
            registries.Delete(registryToDelete);
            _transactionRepository.CommitTransaction(transaction);
        }
        catch (Exception e)
        {
            _transactionRepository.RollbackTransaction(transaction);
            ModelState.AddModelError("response", "something wrong while deleting the user");
            Console.WriteLine(e);
        }
        return Ok(userToDelete);
    }

    #endregion
    
    #endregion
}