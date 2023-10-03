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
}