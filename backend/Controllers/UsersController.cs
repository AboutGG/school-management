using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Linq.Dynamic;
using backend.Dto;
using backend.Interfaces;
using backend.Models;
using backend.Repositories;
using backend.Utils;
using J2N.Text;
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
        SchoolContext context
    )
    {
        this._userRepository = userRepository;
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
    [ProducesResponseType(200, Type = typeof(List<Registry>))]
    public IActionResult GetUsers([FromQuery] PaginationParams @params)
    {
        //check if the order type is valid
        if (@params.OrderType.Trim().ToLower() != "asc" && @params.OrderType.Trim().ToLower() != "desc")
        {
            return BadRequest($"{@params.OrderType} is not a valid order");
        }

        GenericRepository<Registry> registryRepo = new GenericRepository<Registry>(_context);
        //I take all the users using the params element and its includes

        List<Registry> registries = registryRepo.GetAllUsingIQueryable(@params,
            query => query
                .Where(reg => reg.Name.Trim().ToLower().Contains(@params.Search.Trim().ToLower()) ||
                              reg.Surname.Trim().ToLower().Contains(@params.Search.Trim().ToLower()))
                .Include(reg => reg.Student)
                .Include(reg => reg.Teacher),
            out var total
        );

        //if the role is null returns all the users
        if (@params.Filter == null)
        {
            return Ok(new PaginationResponse<Registry>
            {
                Total = total,
                Data = registries
            });
        }

        //if the role is not null return the users which have the role equal then params.role
        switch (@params.Filter.Trim().ToLower())
        {
            case "teacher":
                registries = new GenericRepository<Registry>(_context).GetAllUsingIQueryable(  
                    @params,
                    query => registries.AsQueryable()
                        .Where(reg => reg.Student == null)
                    , out var totalTeacher);
                return Ok(new PaginationResponse<Registry>
                {
                    Total = totalTeacher,
                    Data = registries
                });
            case "student":
                registries = new GenericRepository<Registry>(_context).GetAllUsingIQueryable(  
                    @params,
                    query => registries.AsQueryable()
                        .Where(reg => reg.Teacher == null)
                    , out var totalStudent);
                return Ok(new PaginationResponse<Registry>
                {
                    Total = totalStudent,
                    Data = registries
                });
            default:
                return NotFound($"The Role \"{@params.Filter}\" has not found");
        }
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
    public IActionResult CreateUser([FromHeader] string Token, [FromBody] AddEntity inputUser)
    {
        IDbContextTransaction transaction = _transactionRepository.BeginTransaction();
        try
        {
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
            };

            if (new GenericRepository<Registry>(_context).Create(newRegistry) &&
                new GenericRepository<User>(_context).Create(newUser))
            {

                switch (inputUser.Role.Trim().ToLower())
                {
                    case "student":
                        Student newStudent = new Student
                        {
                            Id = Guid.NewGuid(),
                            UserId = newUser.Id,
                            RegistryId = newRegistry.Id,
                            ClassroomId = inputUser.ClassroomId ?? throw new Exception("UNKNOWN_CLASSROOM"),
                            SchoolYear = SchoolYearUtils.GetCurrentSchoolYear()
                        };

                        if (!new GenericRepository<Student>(_context).Create(newStudent))
                        {
                            throw new Exception("NOT_CREATED");
                        }

                        break;

                    case "teacher":
                        Teacher newTeacher = new Teacher
                        {
                            Id = Guid.NewGuid(),
                            RegistryId = newRegistry.Id,
                            UserId = newUser.Id
                        };

                        if (!new GenericRepository<Teacher>(_context).Create(newTeacher))
                        {
                            throw new Exception("NOT_CREATED");
                        }

                        break;

                    default:
                        throw new Exception("ROLE_NONEXISTENT");

                }

            }
            else
            {
                throw new Exception("NOT_CREATED");
            }

            _transactionRepository.CommitTransaction(transaction);
            return Ok("The user has successfully created");
        }
        catch (Exception e)
        {
            _transactionRepository.RollbackTransaction(transaction);
            ErrorResponse error = ErrorManager.Error(e);
            return StatusCode(error.statusCode, error);
        }
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
    
    #region Me

    [HttpGet]
    [Route("me")]
    public IActionResult GetMe([FromHeader] string token)
    {
        return Ok(_userRepository.GetMe(token));
    }
    
    #endregion

    #region Change password

    /// <summary>
    /// Api call used to change the password of an user
    /// </summary>
    /// <param name="Token">token to check if you are authorized to change the password</param>
    /// <param name="userId">userId to take the user which we want to change the password</param>
    /// <param name="changePasswordDto">Dto used to take in input the new and old password</param>
    /// <returns>Return a message if the password has successfully changed</returns>
    /// <exception cref="Exception">Execeptions to show the error message if we are unauthorized to change the password or if the new password are equal then the old
    /// or the password has not changed.
    /// </exception>
    [HttpPut]
    [HttpPut("{userId}")]
    public IActionResult ChangePassword([FromHeader] string Token, [FromRoute] Guid userId,
        ChangePasswordDto changePasswordDto)
    {
        JwtSecurityToken decodedToken;
        IDbContextTransaction transaction = _transactionRepository.BeginTransaction();
        try
        {
            //Decodifico il token
            decodedToken = JWTHandler.DecodeJwtToken(Token);
            Guid takenId = new Guid(decodedToken.Payload["userid"].ToString());

            //Controllo se gli id passati tramite token e route sono diversi, in quel caso non si è autorizzati
            if (takenId != userId)
            {
                throw new Exception("CHANGE_PASSWORD_ACCESS_DENIED");
            }

            //Prendo lo user a cui devo cambiare la password
            User takenUser = new GenericRepository<User>(_context).GetByIdUsingIQueryable(
                query => query
                    .Where(el => el.Id == userId));
            //Controllo se la vecchia password è giusta
            if (changePasswordDto.oldPassword != takenUser.Password)
            {
                throw new Exception("INVALID_OLD_PASSWORD");
            }
            
            //Effettuo i controlli nel caso in cui la nuova password sia uguale a quella vecchia
            if (changePasswordDto.oldPassword == changePasswordDto.newPassword ||
                takenUser.Password == changePasswordDto.newPassword)
            {
                throw new Exception("EQUALS_PASSWORDS");
            }
            
            //Modifico la password
            takenUser.Password = changePasswordDto.newPassword;

            //Procedo con l'update della password nel Db
            if (!new GenericRepository<User>(_context).UpdateEntity(takenUser))
            {
                throw new Exception("PASSWORD_NOT_CHANGED");
            }

            _transactionRepository.CommitTransaction(transaction);
            return StatusCode(StatusCodes.Status200OK, new MessageResponse("The password has successfully changed"));
        }
        catch (Exception e)
        {
            _transactionRepository.RollbackTransaction(transaction);
            ErrorResponse error = ErrorManager.Error(e);
            return StatusCode(error.statusCode, error);
        }
    }

    #endregion
    
    #endregion
}

