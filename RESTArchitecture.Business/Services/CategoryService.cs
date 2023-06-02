using RESTArchitecture.Common.Interfaces;
using RESTArchitecture.Common.Models;
using RESTArchitecture.DAL.Repositories;

namespace RESTArchitecture.Business.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService()
        {
            _categoryRepository = new CategoryRepository();
        }

        public void Add(CategoryViewModel category)
        {
            _categoryRepository.AddCategory(new Category
            {
                Name = category.Name,
                ItemsIds = category.ItemsIds
            });
        }

        public void Delete(int id)
        {
            _categoryRepository.DeleteById(id);
        }

        public async Task<List<Category>> Get()
        {
            var source = new CancellationTokenSource();
            return await _categoryRepository.GetListOfCategories(source.Token);
        }

        public void Update(Category category)
        {
            _categoryRepository.UpdateCategory(category);
        }
    }
}
