using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PdfController : Controller
{
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok();
    }
}