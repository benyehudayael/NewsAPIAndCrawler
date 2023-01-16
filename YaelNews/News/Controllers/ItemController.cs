using Microsoft.AspNetCore.Mvc;
using News.Model;
using News.Contracts;
using System.Data;

namespace News.Controllers
{
    // https://www.yaelnews.com/api/items
    // rest api
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private IDataService _dataService;

        public ItemController(IDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Item>>> GetItems(int pageSize, int pageIndex, Guid? sid, string? ft)
        {
            var dbItems = await _dataService.GetItems(pageSize, pageIndex, sid, ft);
            var items =  dbItems.Select(x => Mappers.Mapper.ItemToModel(x)).Where(x => x.Image != "");
            return Ok(items);
        }


        [HttpPut]
        public async Task Update(Item item)
        {
       
        }

        [HttpPost]
        public async Task AddNewItem(Item item)
        {
            await Task.Run(() => _dataService.AddNewItem(new DbModel.Item() { Id = item.ID, SubjectId = item.SubjectId, Content = item.Content, CreatedOn = item.CreatedOn, Image = item.Image, Link = item.Link}));
        }
    }
}
