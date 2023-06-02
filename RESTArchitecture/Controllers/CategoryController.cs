using Microsoft.AspNetCore.Mvc;
using RESTArchitecture.Common.Interfaces;
using RESTArchitecture.Common.Models;
using RESTArchitecture.Business.Services;

namespace RESTArchitecture.Controllers
{
    [Route("api/")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController()
        {
            _categoryService = new CategoryService();
        }

        [HttpGet, Route("[controller]/Get")]
        public IEnumerable<Category> Get()
        {
            return _categoryService.Get();
        }

        [HttpPost, Route("[controller]/Add")]
        public void Add(CategoryViewModel category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category), "Submitted model is null.");
            }
            _categoryService.Add(category);
        }

        [HttpPost, Route("[controller]/Update")]
        public void Update(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category), "Submitted model is null.");
            }
            _categoryService.Update(category);
        }

        [HttpDelete, Route("[controller]/Delete")]
        public void Delete(int id)
        {
            _categoryService.Delete(id);
        }
    }
}
