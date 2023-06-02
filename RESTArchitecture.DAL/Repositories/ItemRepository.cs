using System.Linq;
using System.Text.Json;
using RESTArchitecture.Common.Interfaces;
using RESTArchitecture.Common.Models;

namespace RESTArchitecture.DAL.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private const int _pageSize = 10;
        private const string _dbPath = @"C:\RESTDB\Items";
        private const string _fileFormat = @".json";

        public void AddItem(Item item)
        {
            var count = GetCounter();
            item.Id = count;
            var jsonString = JsonSerializer.Serialize(item);
            var path = $"{_dbPath}\\{count}{_fileFormat}";

            if (File.Exists(path))
            {
                throw new ArgumentException($"File with id {count} already exists", path);
            }

            File.WriteAllText(path, jsonString);
        }

        public List<Item> GetItems(int? categoryId, int? page)
        {
            var list = new List<Item>();
            var files = Directory.GetFiles(_dbPath);

            Parallel.ForEach(files, x =>
            {
                var openedFile = File.ReadAllText(x);
                var item = JsonSerializer.Deserialize<Item>(openedFile);
                if (item != null && item.CategoryId == categoryId)
                {
                    list.Add(item);
                }
            });

            return page != null ? list.Skip(page.Value * _pageSize).Take(_pageSize).ToList() : list;
        }

        void IItemRepository.DeleteItem(int id)
        {
            var path = $"{_dbPath}\\{id}{_fileFormat}";

            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Specified file not found", path);
            }

            File.Delete(path);
        }

        void IItemRepository.UpdateItem(Item item)
        {
            var jsonString = JsonSerializer.Serialize(item);
            var path = $"{_dbPath}\\{item.Id}{_fileFormat}";

            if (!File.Exists(path))
            {
                throw new FileNotFoundException("File not found", path);
            }

            File.WriteAllText(path, jsonString);
        }

        private int GetCounter()
        {
            return Directory.GetFiles(_dbPath).Length;
        }
    }
}
