using RESTArchitecture.Common.Models;

namespace RESTArchitecture.Common.Interfaces
{
    public interface IItemService
    {
        public void Add(ItemViewModel item);
        public void Update(Item item);
        public void Delete(int id);
        public List<Item> Get(int? categoryId = null, int? page = null);
    }
}
