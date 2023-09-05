using System.IdentityModel.Tokens.Jwt;
using backend.Interfaces;
using backend.Models;
using backend.Repositories;
using backend.Utils;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ExamsController : Controller
{
    #region Attributes

    private readonly SchoolContext _context;

    #endregion

    #region Constructor

    public ExamsController(SchoolContext context)
        {
            _context = context;
        }

    #endregion
    
    #region Api calls
   

    #endregion
    
}