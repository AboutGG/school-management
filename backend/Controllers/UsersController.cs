using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using backend.Dto;
using backend.Interfaces;
using backend.Models;
using backend.Repositories;
using backend.Utils;
using iText.StyledXmlParser.Jsoup.Parser;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]

public class UsersController : Controller
{
    #region Attributes

    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly SchoolContext _context;
    private readonly IRegistryRepository _registryRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly ITransactionRepository _transactionRepository;

    #endregion

    #region Costructor

    public UsersController(
        ITransactionRepository transactionRepository,
        IUserRepository userRepository,
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
        
        //I take all the users using the params element and its includes

        List<User> users = new GenericRepository<User>(_context).GetAll(@params,
            reg => reg.Registry.Name.Trim().ToLower().Contains(@params.Search.Trim().ToLower()) ||
                   reg.Registry.Surname.Trim().ToLower().Contains(@params.Search.Trim().ToLower()),
            reg => reg.Registry);
        
        //if the role is null returns all the users
        if (@params.Filter == null)
        {
            return Ok(users);
        }
    
        //if the role is not null return the users which have the role equal then params.role
        switch (@params.Filter.Trim().ToLower())
        {
            case "teacher":
                users = users.Where(reg => reg.Student == null).ToList();
                break;
            case "student":
                users = users.Where(reg => reg.Student != null).ToList();
                break;
            default:
                return NotFound($"The Role \"{@params.Filter}\" has not found");
        }

        var mappedUser = _mapper.Map<UserDetailDto>(users);
        return StatusCode(StatusCodes.Status200OK, users);
    }
    
    #endregion
    
    #region Add an User

    /// <summary>
    /// This call is used to create an user and assign to it the role
    /// </summary>
    /// <param name="inputUser"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public IActionResult AddUser([FromHeader] string Token, [FromBody] AddEntity inputUser)
    {

        JwtSecurityToken decodedToken;
        Guid takenId;
        string authorizationRole;
        IDbContextTransaction transaction = _transactionRepository.BeginTransaction();
        try
        {
            //Decode the token
            decodedToken = JWTHandler.DecodeJwtToken(Token);
            takenId = new Guid(decodedToken.Payload["userid"].ToString());

            //Controllo il ruolo dello User tramite l'Id
            authorizationRole = RoleSearcher.GetRole(takenId, _context);

            if (authorizationRole.Trim().ToLower() != "administrator")
            {
                throw new Exception("UNAUTHORIZED");
            }

            if (new GenericRepository<User>(_context).Exist(el => el.Username == inputUser.User.Username))
            {
                throw new Exception("USERNAME_EXISTS");
            }
            
            ///<summary> Create the Registry to add on db, taking the attributes fro= inputUser</summary>
            var newRegistry = new Registry
            {
                Id = Guid.NewGuid(),
                Name = inputUser.Registry.Name,
                Surname = inputUser.Registry.Surname,
                Birth = inputUser.Registry.Birth ?? null,
                Gender = inputUser.Registry.Gender,
                Email = inputUser.Registry.Email ?? null,
                Address = inputUser.Registry.Address ?? null,
                Telephone = inputUser.Registry.Telephone ?? null,
            };
            
            //Create the user to add on db, taking the attributes from inputUser
            
            var newUser = new User
            {
                Id = Guid.NewGuid(),
                Username = inputUser.User.Username,
                Password = inputUser.User.Password,
                RegistryId = newRegistry.Id
            };
            
            if (new GenericRepository<Registry>(_context).Create(newRegistry) && new GenericRepository<User>(_context).Create(newUser))
            {
                UserRole newUserRole;
                string roleName = new GenericRepository<Role>(_context)
                    .GetByIdUsingIQueryable(el =>
                    el.Where(el => el.Id == inputUser.RoleId)).Name;
                
                newUserRole = new UserRole
                {
                    RoleId = inputUser.RoleId,
                    UserId = newUser.Id
                };

                if (roleName == "student")
                {
                    Student newStudent = new Student
                    {
                        UserId = newUser.Id,
                        ClassroomId = inputUser.ClassroomId ?? throw new Exception("UNKNOWN_CLASSROOM")
                    };

                    if (!new GenericRepository<Student>(_context).Create(newStudent))
                    {
                        throw new Exception("NOT_CREATED");
                    }
                }

                if (!new GenericRepository<UserRole>(_context).Create(newUserRole))
                {
                    throw new Exception("NOT_CREATED");
                }
                
                _transactionRepository.CommitTransaction(transaction);
                return Ok("The user has successfully created");
            }

            throw new Exception("NOT_CREATED");
        }
        catch (Exception e)
        {
            _transactionRepository.RollbackTransaction(transaction);
            ErrorResponse error = ErrorManager.Error(e);
            return StatusCode(error.statusCode, error);
        }
    }

    #endregion
    //
    // #region Add an UserStudent
    //
    // /// <summary>
    // /// This call is used to create an user which is a Teacher
    // /// </summary>
    // /// <param name="userDetail"></param>
    // /// <returns></returns>
    // [HttpPost("student")]
    // [ProducesResponseType(204)]
    // [ProducesResponseType(400)]
    // public IActionResult AddUserStudent([FromBody] AddEntity userStudent)
    // {
    //     if (userStudent == null
    //         || userStudent.Classroom == null
    //         || userStudent.User == null ||
    //         userStudent.Registry == null
    //        )
    //     {
    //         return BadRequest(ModelState);
    //     }
    //
    //
    //     if (_userRepository.UserExists(userStudent.User.Username))
    //     {
    //         ModelState.AddModelError("response", "User already exist");
    //         return StatusCode(422, ModelState);
    //     }
    //
    //     ///<summary> start transaction </summary>
    //     var transaction = _transactionRepository.BeginTransaction();
    //
    //     ///<summary> Create the user to add on db, taking the attributes from userStudent</summary>
    //     var user = new User
    //     {
    //         Id = new Guid(),
    //         Username = userStudent.User.Username,
    //         Password = userStudent.User.Password,
    //     };
    //
    //     ///<summary> Create the Registry to add on db, taking the attributes from userStudent</summary>
    //     var registry = new Registry
    //     {
    //         Id = new Guid(),
    //         Name = userStudent.Registry.Name,
    //         Surname = userStudent.Registry.Surname,
    //         Birth = userStudent.Registry.Birth ?? null,
    //         Gender = userStudent.Registry.Gender,
    //         Email = userStudent.Registry.Email ?? null,
    //         Address = userStudent.Registry.Address ?? null,
    //         Telephone = userStudent.Registry.Telephone ?? null,
    //     };
    //
    //     ///<summary> Try to create an user and registry</summary>
    //     if (_userRepository.CreateUser(user) && _registryRepository.CreateRegistry(registry))
    //     {
    //         var student = new Student()
    //         {
    //             Id = new Guid(),
    //             UserId = user.Id,
    //             RegistryId = registry.Id,
    //             ClassroomId = userStudent.Classroom.Id,
    //         };
    //
    //         if (this._studentRepository.CreateStudent(student))
    //         {
    //             _transactionRepository.CommitTransaction(transaction);
    //             return Ok(student);
    //         }
    //         else
    //         {
    //             _transactionRepository.RollbackTransaction(transaction);
    //             return BadRequest(ModelState);
    //         }
    //     }
    //     else
    //     {
    //         _transactionRepository.RollbackTransaction(transaction);
    //         ModelState.AddModelError("Response", "Student is null");
    //         return BadRequest(ModelState);
    //     }
    // }
    //
    // #endregion
    //
    // #region Pdf for circular and table
    //
    // [HttpPost]
    // [Route("pdf")]
    // public IActionResult GetUsersOnPdf([FromBody] Circular? data, [FromQuery] string type = "table")
    // {
    //     ///<summary>We return a Bytes array because the PDF is a sequence of binary bytes to represent the document content compactly. </summary>
    //    
    //     var stream = PDF.GeneratePdf(type,  _mapper.Map<List<UserDto>>(_userRepository.GetUsers()), data);
    //     
    //     // Returns the PDF
    //     return File(stream, "application/pdf", "generated.pdf");
    //
    // }
    //
    // #endregion
    //
    // #region Delete an User
    //
    // /// <summary> Delete a using an id </summary>
    // /// <param name="id">the id of an user which we delete</param>
    // /// <returns>Response ok if deleted, not found if the id not exists</returns>
    // [HttpDelete("{id}")]
    // [ProducesResponseType(400)]
    // [ProducesResponseType(204)]
    // [ProducesResponseType(404)]
    // public IActionResult DeleteUser(Guid id)
    // {
    //     GenericRepository<User> users = new GenericRepository<User>(_context);
    //     GenericRepository<Registry> registries = new GenericRepository<Registry>(_context);
    //     
    //     var transaction = _transactionRepository.BeginTransaction();
    //     if (!users.Exist(user => user.Id == id))
    //     {
    //         return NotFound();
    //     }
    //
    //     User userToDelete = users.GetById(user => user.Id == id, user => user.Student, user => user.Teacher);
    //     Registry registryToDelete = registries.GetById(reg => reg.Id == userToDelete.Student.RegistryId);
    //
    //     //When i can't delete an Entity it returns a Throw exception error then i can rollback all
    //     try
    //     {
    //         users.Delete(userToDelete);
    //         registries.Delete(registryToDelete);
    //         _transactionRepository.CommitTransaction(transaction);
    //     }
    //     catch (Exception e)
    //     {
    //         _transactionRepository.RollbackTransaction(transaction);
    //         ModelState.AddModelError("response", "something wrong while deleting the user");
    //         Console.WriteLine(e);
    //     }
    //     return Ok(userToDelete);
    // }
    //
    // #endregion
    //

    [HttpGet("me")]
    [ProducesResponseType(400)]
    public IActionResult GetMyDetails([FromHeader] string token)
    {
        var loggedUserId = Guid.Parse(JWTHandler.DecodeJwtToken(token).Payload["userid"].ToString());
        var user = new GenericRepository<User>(_context)
            .GetByIdUsingIQueryable(
                query => query
                    .Where(el => el.Id == loggedUserId)
                    .Include(el => el.Student)
                    .Include(el => el.TeachersSubjectsClassrooms));
        return Ok(user);
    }
    #endregion
}