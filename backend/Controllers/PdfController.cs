using backend.Dto;
using backend.Models;
using backend.Repositories;
using backend.Utils;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PdfController : Controller
{
    private readonly SchoolContext _context;

    public PdfController(SchoolContext context)
    {
        _context = context;
    }
    
    [HttpPost]
    [Route("Circulars")]
    [ProducesResponseType(200, Type = typeof(CircularRequest))]
    public IActionResult GetAll([FromBody] CircularRequest circular)
    {
        Circular c = new ()
        {
            CircularNumber = circular.number,
            UploadDate = circular.date,
            Location = circular.location,
            Header = circular.header,
            Body = circular.body,
            Sign = circular.sign
        };

        var pdf = PdfHandler.GeneratePdf("circular", null, c);
        
        return File(pdf, "application/pdf", "generated.pdf");
    }
}