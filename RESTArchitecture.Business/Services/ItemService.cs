using RESTArchitecture.Common.Interfaces;
using RESTArchitecture.Common.Models;
using RESTArchitecture.DAL.Repositories;

namespace RESTArchitecture.Business.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        public ItemService()
        {
            _itemRepository = new ItemRepository();
        }

        public void Add(ItemViewModel item)
        {
            _itemRepository.AddItem(new Item
            {
                CategoryId = item.CategoryId, 
                Name = item.Name
            });
        }

        public void Delete(int id)
        {
            _itemRepository.DeleteItem(id);
        }

        public async Task<List<Item>> Get(int? categoryId, int? page)
        {
            return await _itemRepository.GetItems(categoryId, page);
        }

        public void Update(Item item)
        {
            _itemRepository.UpdateItem(item);
        }
    }
}
