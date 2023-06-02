using Microsoft.AspNetCore.Mvc;
using RESTArchitecture.Business.Services;
using RESTArchitecture.Common.Interfaces;
using RESTArchitecture.Common.Models;

namespace RESTArchitecture.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemController()
        {
            _itemService = new ItemService();
        }

        [HttpGet, Route("[controller]/Get")]
        public IEnumerable<Item> Get(int? categoryId, int? page)
        {
            return _itemService.Get(categoryId, page);
        }

        [HttpPost, Route("[controller]/Add")]
        public void Add(ItemViewModel item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Submitted value is null.");
            }
            _itemService.Add(item);
        }

        [HttpPost, Route("[controller]/Update")]
        public void Update(Item item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Submitted value is null.");
            }
            _itemService.Update(item);
        }

        [HttpDelete, Route("[controller]/Delete")]
        public void Delete(int id)
        {
            _itemService.Delete(id);
        }
    }
}
