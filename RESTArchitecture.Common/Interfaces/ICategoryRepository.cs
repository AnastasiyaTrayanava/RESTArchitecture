using RESTArchitecture.Common.Models;

namespace RESTArchitecture.Common.Interfaces
{
    public interface ICategoryRepository
    {
        public void AddCategory(Category category);
        public void UpdateCategory(Category category);
        public Task<List<Category>> GetListOfCategories(CancellationToken token);
        public void DeleteById(int id);
    }
}
