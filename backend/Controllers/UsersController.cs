using AutoMapper;
using backend.Dto;
using backend.Interfaces;
using backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : Controller
{
    #region Attributes

    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
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
        IMapper mapper
    )
    {
        this._userRepository = userRepository;
        this._mapper = mapper;
        this._transactionRepository = transactionRepository;
        this._teacherRepository = teacherRepository;
        this._registryRepository = registryRepository;
        this._studentRepository = studentRepository;
    }

    #endregion

    #region API calls

    #region Get all users

    /// <summary> get call on user breakpoint </summary>
    /// <returns>All User</returns>
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
    public IActionResult GetUsers()
    {
        return Ok(_mapper.Map<List<UserDto2>>(_userRepository.GetUsers()));
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
}