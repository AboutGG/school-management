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

    /// <summary> Get the user's detail </summary>
    /// <param name="Id"></param>
    /// <returns>The details of a single user</returns>
    [HttpGet("{Id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public IActionResult GetUserDetail(Guid Id)
    {
        if (string.IsNullOrWhiteSpace(Convert.ToString(Id)))
            return BadRequest("Invalid Id");
        var users = new GenericRepository<User>(_context);
        if (users.Exist(u => u.Id == Id))
        {
            var takenUser = users.GetById(u => u.Id == Id,
                u => u.Teacher.Registry,
                u => u.Student.Registry);

            return Ok(takenUser?.Student.Registry == null ? takenUser.Teacher.Registry : takenUser.Student.Registry);
        }

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
    public IActionResult UpdateUser(Guid Id,
        [FromBody] RegistryDto updatedRegistry) // i pass the user's Id
    {
        if (updatedRegistry == null || Id == null)
            return BadRequest();

        GenericRepository<Registry> registryRepo = new GenericRepository<Registry>(_context);

        // take the registry which have the Teacher || Student .UserId == Id
        Registry takenRegistry = registryRepo.GetById(
            reg => reg.Student.UserId == Id || reg.Teacher.UserId == Id,
            reg => reg.Student,
            reg => reg.Teacher
        );

        //start transaction
        var transaction = _transactionRepository.BeginTransaction();

        //Update taken registry
        takenRegistry.Name = updatedRegistry.Name;
        takenRegistry.Surname = updatedRegistry.Surname;
        takenRegistry.Birth = updatedRegistry.Birth;
        takenRegistry.Address = updatedRegistry.Address;
        takenRegistry.Email = updatedRegistry.Email;
        takenRegistry.Gender = updatedRegistry.Gender;
        takenRegistry.Telephone = updatedRegistry.Telephone;

        try
        {
            registryRepo.UpdateEntity(takenRegistry); //update the user's registry
            _transactionRepository.CommitTransaction(transaction); //accept the changes
            return Ok("Edit successfully");
        }
        catch (Exception e)
        {
            //rollback when i can't update an Entity
            _transactionRepository.RollbackTransaction(transaction);
            ModelState.AddModelError("response", "Something went wrong updating user");
            return StatusCode(500, ModelState);
        }
    }

    #endregion

    #region Detail count

    /// <summary> Function which gives the Users, Students, Teachers and Classrooms' counts </summary>
    /// <returns>Return the Users, Students, Teachers and Classrooms' number</returns>
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
            { "Classrooms", _classroomRepository.GetClassroomsCount() }
        });
    }

    #endregion

    #endregion
}