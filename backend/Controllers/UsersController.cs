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
    [ProducesResponseType(200, Type = typeof(List<UserList>))]
    public IActionResult GetUsers([FromQuery] PaginationParams @params, [FromHeader] string token)
    {
        // //if the role is not null return the users which have the role equal then params.role
        // switch (@params.Filter.Trim().ToLower())
        // {
        //     case "teacher":
        //         users = users.Where(reg => reg.Student == null).ToList();
        //         break;
        //     case "student":
        //         users = users.Where(reg => reg.Student != null).ToList();
        //         break;
        //     default:
        //         return NotFound($"The Role \"{@params.Filter}\" has not found");
        // }

        var users = new GenericRepository<User>(_context)
            .GetAllUsingIQueryable(
                @params,
                queryFunc => queryFunc
                    .Where(el => 
                        el.Registry.Name.Trim().ToLower().Contains(@params.Search.Trim().ToLower())
                        || 
                        el.Registry.Surname.Trim().ToLower().Contains(@params.Search.Trim().ToLower()))
                    .Include(u => u.Registry));
        var prova = _mapper.Map<List<UserList>>(users);
        return Ok(prova);
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
    
    #region Get Registry of user

    [HttpGet]
    [Route("{id}")]
    public IActionResult GetRegistry([FromRoute] Guid id, [FromHeader] string token)
    {
        var user = new GenericRepository<User>(_context)
            .GetByIdUsingIQueryable(query => query
                .Include(el => el.Registry)
                .Include(el => el.UsersRoles)
                .ThenInclude(el => el.Role)
                .Include(el => el.Student.Classroom)
                .Include(el => el.TeachersSubjectsClassrooms)
                .ThenInclude(el => el.Classroom));
                
        var request = _mapper.Map<UserDetailDto>(user);
        return Ok(request);
    }
    
    #endregion
    
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

    #region Get user details

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


    #region Edit detail

    //TODO: add role control
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
        //start transaction
        var transaction = _transactionRepository.BeginTransaction();
        try
        {
            if (updatedRegistry == null || Id == null)
                throw new Exception("INVALID_PARAMETERS");

            // take the user which we need to update
            Registry takenRegistry = new GenericRepository<User>(_context)
                .GetByIdUsingIQueryable(
                    query => query
                        .Where(user => user.Id == Id)
                        .Include(user => user.Registry)).Registry;

            //Update taken registry
            takenRegistry.Name = updatedRegistry.Name;
            takenRegistry.Surname = updatedRegistry.Surname;
            takenRegistry.Birth = updatedRegistry.Birth;
            takenRegistry.Address = updatedRegistry.Address;
            takenRegistry.Email = updatedRegistry.Email;
            takenRegistry.Gender = updatedRegistry.Gender;
            takenRegistry.Telephone = updatedRegistry.Telephone;


            if (!new GenericRepository<Registry>(_context).UpdateEntity(takenRegistry)) //update the user's registry
            {
                throw new Exception("NOT_UPDATED");
            }

            _transactionRepository.CommitTransaction(transaction); //accept the changes
            return Ok("Edit successfully");
        }
        catch (Exception e)
        {
            //rollback when i can't update an Entity
            _transactionRepository.RollbackTransaction(transaction);
            ErrorResponse error = ErrorManager.Error(e);
            return StatusCode(error.statusCode, error);
        }
    }

    #endregion

    #endregion
}