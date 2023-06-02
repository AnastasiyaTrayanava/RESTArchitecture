using RESTArchitecture.Common.Models;

namespace RESTArchitecture.Common.Interfaces
{
    public interface IItemRepository
    {
        public List<Item> GetItems(int? categoryId, int? page);
        public void UpdateItem(Item item);
        public void DeleteItem(int id);
        public void AddItem(Item item);
    }
}
