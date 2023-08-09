using AutoMapper;
using backend.Dto;
using backend.Interfaces;
using backend.Models;
using backend.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DetailsController : Controller
{
    #region Attributes

    private readonly IMapper _mapper;
    private readonly IStudentRepository _studentRepository;
    private readonly ITeacherRepository _teacherRepository;
    private readonly IRegistryRepository _registryRepository;
    private readonly IUserRepository _userRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IClassroomRepository _classroomRepository;
    private readonly SchoolContext _context;

    #endregion

    #region Costructor

    public DetailsController(IMapper mapper,
        IStudentRepository studentRepository,
        ITeacherRepository teacherRepository,
        IRegistryRepository registryRepository,
        IUserRepository userRepository,
        ITransactionRepository transactionRepository,
        IClassroomRepository classroomRepository,
        SchoolContext context)
    {
        _mapper = mapper;
        _studentRepository = studentRepository;
        _teacherRepository = teacherRepository;
        _registryRepository = registryRepository;
        _userRepository = userRepository;
        _transactionRepository = transactionRepository;
        _classroomRepository = classroomRepository;
        _context = context;
    }

    #endregion

    #region Api calls

    #region Get user detail by id

    [HttpGet("{Id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public IActionResult GetUserDetail(Guid Id)
    {
        if (string.IsNullOrWhiteSpace(Convert.ToString(Id)))
            return BadRequest("Invalid Id");
        
        var registries = new GenericRepository<Registry>(_context);
        if (_registryRepository.RegistryExists(Id))
            return Ok(registries.GetById(r => r.Id == Id,
                r => r.Teacher.User,
                r => r.Student.User)
            );
        return NotFound();
    }

    #endregion

    #region Edit detail

    /// <summary> Edit the Teacher or Student details. </summary>
    /// <param name="Id"></param>
    /// <param name="updatedUserDetail"></param>
    /// <returns>204 = Successfully, 404 = not found the id, 400 = bad request</returns>
    [HttpPut("{Id}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public IActionResult PutUserDetail(Guid Id,
        [FromBody] UserDetailDto updatedUserDetail) // i pass the id of the teacher or student
    {
        if (updatedUserDetail == null || Id == null || updatedUserDetail.User == null ||
            updatedUserDetail.Registry == null)
            return BadRequest();

        //start transaction
        var transaction = _transactionRepository.BeginTransaction();
        if (!_userRepository.UserExists(updatedUserDetail.User.Username) &&
            (_studentRepository.StudentExist(Id) || _teacherRepository.TeacherExists(Id)))
        {
            //take student or teacher
            var teacher = _teacherRepository.GetTeacherById(Id);
            var student = _studentRepository.GetStudentById(Id);
            if (teacher == null || student == null)
            {
                return NotFound();
            }

            //create new registry and user
            Registry registry = new Registry()
            {
                Id = teacher?.RegistryId ?? student.RegistryId,
                Name = updatedUserDetail.Registry.Name,
                Surname = updatedUserDetail.Registry.Surname,
                Birth = updatedUserDetail.Registry.Birth,
                Address = updatedUserDetail.Registry.Address,
                Email = updatedUserDetail.Registry.Email,
                Gender = updatedUserDetail.Registry.Gender,
                Telephone = updatedUserDetail.Registry.Telephone,

            };
            var user = new User()
            {
                Id = teacher?.UserId ?? student.UserId,
                Username = updatedUserDetail.User.Username,
                Password = updatedUserDetail.User.Password,
            };
            //update registry and user
            if (_registryRepository.UpdateRegistry(registry) &&
                _userRepository.UpdateUser(user))
            {
                //accept the changes
                _transactionRepository.CommitTransaction(transaction);
                return Ok("Edit successfully");
            }
            else
            {
                //rollback when i can't update an Entity
                _transactionRepository.RollbackTransaction(transaction);
                ModelState.AddModelError("response", "Something went wrong updating user");
                return StatusCode(500, ModelState);
            }
        }

        return BadRequest("username already exist");
    }

    #endregion

    #region Detail count

    [HttpGet("count")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public IActionResult GetCount()
    {
        return Ok(new Dictionary<string, int>
        {
            { "Users", _userRepository.CountUsers() },
            { "Students", _studentRepository.CountStudents() },
            { "Teachers", _teacherRepository.CountTeachers() },
            { "Classrooms", _classroomRepository.GetClassrooms() }
        });
    }

    #endregion

    #endregion
}