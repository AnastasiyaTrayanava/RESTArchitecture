using RESTArchitecture.Common.Models;

namespace RESTArchitecture.Common.Interfaces
{
    public interface ICategoryRepository
    {
        public void AddCategory(Category category);
        public void UpdateCategory(Category category);
        public List<Category> GetListOfCategories();
        public void DeleteById(int id);
    }
}
