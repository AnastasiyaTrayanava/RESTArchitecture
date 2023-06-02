using RESTArchitecture.Common.Models;

namespace RESTArchitecture.Common.Interfaces
{
    public interface ICategoryService
    {
        public void Add(CategoryViewModel category);
        public void Update(Category category);
        public void Delete(int id);
        public Task<List<Category>> Get();
    }
}
