using Microsoft.AspNetCore.Mvc;
using NewsApp.ApplicationLayer.Interfaces;

namespace NewsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsAPIController : ControllerBase
    {
        private readonly INewsService _newsService;

        public NewsAPIController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet("get-all-news")]
        public async Task<IActionResult> GetAllNews([FromQuery] int pageindex, [FromQuery] int pagesize, [FromQuery] string categorySlug)
        {
            try
            {
                var result = await _newsService.GetPaginatedNewsAsync(pageindex, pagesize, categorySlug);
                return Ok(new { news = result.Items, count = result.TotalCount });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("retrieve-new-by-slug")]
        public async Task<IActionResult> GetNewBySlugs([FromQuery] string Categoryslug, [FromQuery] string NewSlug)
        {
            try
            {
                var result = await _newsService.GetBySlugsAsync(Categoryslug, NewSlug);
                return Ok(new { New = result });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
