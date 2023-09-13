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
    private readonly IRegistryRepository _registryRepository;
    private readonly IUserRepository _userRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IClassroomRepository _classroomRepository;
    private readonly SchoolContext _context;

    #endregion

    #region Costructor

    public DetailsController(IMapper mapper,
        IStudentRepository studentRepository,
        IRegistryRepository registryRepository,
        IUserRepository userRepository,
        ITransactionRepository transactionRepository,
        IClassroomRepository classroomRepository,
        SchoolContext context)
    {
        _mapper = mapper;
        _studentRepository = studentRepository;
        _registryRepository = registryRepository;
        _userRepository = userRepository;
        _transactionRepository = transactionRepository;
        _classroomRepository = classroomRepository;
        _context = context;
    }

    #endregion

    // #region Api calls
    //
    // #region Get user detail by id
    //
    // /// <summary> Get the user's detail </summary>
    // /// <param name="Id"></param>
    // /// <returns>The details of a single user</returns>
    // [HttpGet("{Id}")]
    // [ProducesResponseType(200, Type = typeof(RegistryDto))]
    // [ProducesResponseType(404)]
    // [ProducesResponseType(400)]
    // public IActionResult GetUserDetail([FromRoute]Guid Id)
    // {
    //     var role = RoleSearcher.GetRole(Id, _context);
    //     Registry response = null;
    //     if (role == "teacher")
    //     {
    //         response = new GenericRepository<Teacher>(_context)
    //             .GetAllUsingIQueryable(null,
    //                 query => query
    //                     .Include(t => t.Registry)
    //             ).FirstOrDefault(u => u.UserId ==  Id).Registry;
    //     }
    //
    //     if (role == "student")
    //     {
    //         response = new GenericRepository<Student>(_context)
    //             .GetAllUsingIQueryable(null,
    //                 query => query
    //                     .Include(t => t.Registry)
    //             ).FirstOrDefault(u => u.UserId ==  Id).Registry;
    //     }
    //
    //     if (response == null)
    //     {
    //         return NotFound();
    //     }
    //
    //     var x = new 
    //     {
    //         name = response.Name,
    //         surname = response.Surname,
    //         gender = response.Gender,
    //         email = response.Email,
    //         telephone = response.Telephone,
    //         address = response.Address,
    //         birth = response.Birth.ToString()
    //     };
    //     return Ok(x);
    // }
    //
    // #endregion
    
    
    // #region Detail count
    //
    // /// <summary> Function which gives the Users, Students, Teachers and Classrooms' counts </summary>
    // /// <returns>Return the Users, Students, Teachers and Classrooms' number</returns>
    // [HttpGet("count")]
    // [ProducesResponseType(200)]
    // [ProducesResponseType(400)]
    // public IActionResult GetCount()
    // {
    //     return Ok(new Dictionary<string, int>
    //     {
    //         { "Users", _userRepository.CountUsers() },
    //         { "Students", _studentRepository.CountStudents() },
    //         { "Teachers", _teacherRepository.CountTeachers() },
    //         { "Classrooms", _classroomRepository.GetClassroomsCount() }
    //     });
    // }
    //
    // #endregion
    //
    // #endregion
}