using backend.Dto;
using backend.Interfaces;
using backend.Models;
using backend.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;


namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PromotionsHistoryController : Controller
{
    #region Attributes

    private readonly ITransactionRepository _transactionRepository;
    private readonly SchoolContext _context;
    
    #endregion

    #region Costructor

    public PromotionsHistoryController(ITransactionRepository transactionRepository, SchoolContext context)
    {
        _transactionRepository = transactionRepository;
        _context = context;
    }

    #endregion

    #region Api calls

    #region GetHistory
    /// <summary>
    /// Api call used to take all the promotion history of a classroom
    /// </summary>
    /// <param name="classroomId"></param>
    /// <param name="schoolYear">To take a class on the </param>
    /// <returns></returns>
    [HttpGet]
    [Route("classrooms/{classroomId}/students")]
    [ProducesResponseType(200, Type = typeof(List<PromotionHistoryDto>))]
    public IActionResult GetPromotionHistory([FromRoute] Guid classroomId, [FromQuery]string? schoolYear)
    {
        try
        {
            List<PromotionHistory> takenPromotionHistories = new GenericRepository<PromotionHistory>(_context)
                .GetAllUsingIQueryable(null, query => query
                        .Where(el => el.PreviousClassroomId == classroomId)
                    , out var total);
            if (!schoolYear.IsNullOrEmpty())
            {
                takenPromotionHistories = new GenericRepository<PromotionHistory>(_context)
                    .GetAllUsingIQueryable(null, query => takenPromotionHistories.AsQueryable()
                            .Where(el => el.PreviousSchoolYear == schoolYear)
                        , out total);
            }

            List<PromotionHistoryDto> response = new List<PromotionHistoryDto>();
            foreach (PromotionHistory promotionHistory in takenPromotionHistories)
            {
                response.Add(new PromotionHistoryDto(promotionHistory));
            }
            return StatusCode(StatusCodes.Status200OK, response);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    #endregion

    #endregion
    
}