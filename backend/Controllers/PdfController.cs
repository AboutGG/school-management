using backend.Dto;
using backend.Interfaces;
using backend.Models;
using backend.Repositories;
using backend.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PdfController : Controller
{
    private readonly SchoolContext _context;
    private readonly ITransactionRepository _transactionRepository;

    public PdfController(SchoolContext context, ITransactionRepository transactionRepository)
    {
        _context = context;
        _transactionRepository = transactionRepository;
    }

    #region Create Circular

    [HttpPost]
    [Route("Circulars")]
    [ProducesResponseType(200, Type = typeof(CircularRequest))]
    public IActionResult CreateCircular([FromBody] CircularRequest circular)
    {
        IDbContextTransaction transaction = _transactionRepository.BeginTransaction();
        
        try
        {
            Circular c = new()
            {
                CircularNumber = circular.CircularNumber,
                UploadDate = circular.UploadDate,
                Location = circular.location,
                Object = circular.Object,
                Header = circular.header,
                Body = circular.body,
                Sign = circular.sign
            };

            if (new GenericRepository<Circular>(_context).Create(c))
            {
                _transactionRepository.CommitTransaction(transaction);
                var pdf = PdfHandler.GeneratePdf<Circular>(c, null, null, null);
                return File(pdf, "application/pdf", "generated.pdf");
            }
            else
            {
                throw new Exception("NOT_CREATED");
            }
        }
        catch (Exception e)
        {
            _transactionRepository.RollbackTransaction(transaction);
            ErrorResponse error = ErrorManager.Error(e);
            return StatusCode(error.statusCode, error);
        }
    }

    #endregion

    #region Get circulars
    [HttpGet]
    [Route("circulars")]
    [ProducesResponseType(200, Type = typeof(List<Circular>))]
    [ProducesResponseType(401)]
    public IActionResult GetCirculars([FromQuery] PaginationParams @params,[FromRoute] int circularNumber)
    {
        
        try
        {
            List<Circular> takenCircular = new GenericRepository<Circular>(_context)
                .GetAllUsingIQueryable(@params, query => query
                , out var total
                );
            
            return StatusCode(StatusCodes.Status200OK, new PaginationResponse<Circular>()
            {
                Total = total,
                Data = takenCircular
            });
        }
        catch (Exception e)
        {
            ErrorResponse error = ErrorManager.Error(e);
            return StatusCode(error.statusCode, error);
        }
    }

    #endregion
    
    #region Get circular
    [HttpGet]
    [Route("circulars/{circularId}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    public IActionResult GetCircular([FromRoute] Guid circularId)
    {
        
        try
        {
            var takenCircular = new GenericRepository<Circular>(_context)
                .GetByIdUsingIQueryable(query => query
                .Where(el => el.Id == circularId));

            if (takenCircular == null)
            {
                throw new Exception("NOT_FOUND");
            }
            
            var pdf = PdfHandler.GeneratePdf<Circular>(takenCircular, null, null, null);
            
            return File(pdf, "application/pdf", $"circular_n°{circularId}.pdf");
        }
        catch (Exception e)
        {
            ErrorResponse error = ErrorManager.Error(e);
            return StatusCode(error.statusCode, error);
        }
    }

    #endregion
   
    
}