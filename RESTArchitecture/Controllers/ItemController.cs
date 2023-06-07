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

        [HttpGet, Route("[controller]/GetList")]
        public async Task<ActionResult<IEnumerable<Item>>> GetList([FromQuery]ItemSearchParameters search)
        {
            try
            {
                return Ok(await _itemService.Get(search.CategoryId, search.Page));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost, Route("[controller]/Add")]
        public IActionResult Add(ItemViewModel item)
        {
            try
            {
                if (item == null)
                {
                    throw new ArgumentNullException(nameof(item), "Submitted value is null.");
                }
                _itemService.Add(item);
                return Ok();
            }
            catch (ArgumentNullException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost, Route("[controller]/Update")]
        public IActionResult Update(Item item)
        {
            try
            {
                if (item == null)
                {
                    throw new ArgumentNullException(nameof(item), "Submitted value is null.");
                }

                _itemService.Update(item);
                return Ok();
            }
            catch (ArgumentNullException e)
            {
                return BadRequest(e.Message);
            }
            catch (FileNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete, Route("[controller]/Delete")]
        public IActionResult Delete(int id)
        {
            try
            {
                _itemService.Delete(id);
                return Ok();
            }
            catch (FileNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
