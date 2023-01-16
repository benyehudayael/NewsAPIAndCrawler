using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using News.Contracts;
using News.Model;

namespace News.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SourceController : ControllerBase
    {
        private IDataService _dataService;

        // Constructor with Dependency Injection of DataService
        // עדגאיטאכגעיחעכב
        public SourceController(IDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Source>>> GetSources()
        {
            var items = await _dataService.GetSources();
            return Ok(items);
        }

        [HttpPut]
        public async Task Update(Source item)
        {

        }

        [HttpPost]
        public async Task AddNewItem(Source item)
        {

        }
    }
}
