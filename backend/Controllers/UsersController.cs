using System.Linq.Expressions;
using AutoMapper;
using backend.Dto;
using backend.Interfaces;
using backend.Models;
using backend.Repositories;
using Microsoft.AspNetCore.Authorization;
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

        if (@params.Role == null)
        {
            var registries = new GenericRepository<Registry>(_context);
            var registryLambda = GetOrderStatement<Registry>(@params.Order);
            return Ok(registries.GetAll(@params, registry =>
                    registry.Name.Trim().ToLower().Contains(@params.Search)
                    || registry.Surname.Trim().ToLower()
                        .Contains(@params.Search),
                registryLambda
            ));
        }

        switch (@params.Role.Trim().ToLower())
        {
            case "teacher":
                var teachers = new GenericRepository<Teacher>(_context);
                var teacherLambda = GetOrderStatement<Teacher>(@params.Order);
                return Ok(teachers.GetAll(@params, teacher =>
                        teacher.Registry.Name.Trim().ToLower().Contains(@params.Search)
                        || teacher.Registry.Surname.Trim().ToLower()
                            .Contains(@params.Search),
                    teacherLambda,
                    teacher => teacher.User, teacher => teacher.Registry
                ));
            case "student":
                var students = new GenericRepository<Student>(_context);
                var studentLambda = GetOrderStatement<Student>(@params.Order);
                return Ok(students.GetAll(@params, student =>
                        student.Registry.Name.Trim().ToLower().Contains(@params.Search) //contains
                        || student.Registry.Surname.Trim().ToLower().Contains(@params.Search),
                    studentLambda, //OrderBy
                    student => student.User, student => student.Registry //includes params
                ));
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

    #endregion

    #region Other methods

    /// <summary>
    /// Dating a property name it return a lambda which gain an Order Statement
    /// </summary>
    /// <param name="propName"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    private static  Func<T, string> GetOrderStatement<T>(string propName)
    {
        var type = Expression.Parameter(typeof(T), "iesim"); //expression parameter

        Expression property;
        if (typeof(T) == typeof(Student) || typeof(T) == typeof(Teacher)) //check if is a Teacher or Student
        {
            var registryProperty = Expression.PropertyOrField(type, "Registry"); //expression to access to Registry property
            property = Expression.PropertyOrField(registryProperty, propName.Trim()); //Expression to access the attribute name contained in propName within Registry.
        }
        else
        {
            property = Expression.PropertyOrField(type, propName);//same
        }
        
        return Expression.Lambda<Func<T, string>>(property, type).Compile();
    }

    #endregion
    
    
}