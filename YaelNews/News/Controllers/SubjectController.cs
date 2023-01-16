using Microsoft.AspNetCore.Mvc;
using News.Model;
using News.Contracts;

namespace News.Controllers
{
    // https://www.yaelnews.com/api/subject
    // rest api
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private IDataService _dataService;

        public SubjectController(IDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Subject>>> GetSubject()
        {
            var subjects = await _dataService.GetSubjects();
            return Ok(subjects);
        }

        [HttpPut]
        public async Task Update(Item item)
        {

        }

        [HttpPost]
        public async Task AddNewItem(Item item)
        {

        }
    }
}
