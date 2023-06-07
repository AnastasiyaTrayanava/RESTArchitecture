using System.Text.Json;
using Newtonsoft.Json.Linq;
using RESTArchitecture.Common.Interfaces;
using RESTArchitecture.Common.Models;
using RESTArchitecture.Common.Utils;

namespace RESTArchitecture.DAL.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private const int _pageSize = 10;
        private const string _dbPath = @"C:\RESTDB\Items";
        private const string _fileFormat = @".json";

        public void AddItem(Item item)
        {
            var count = Calculations.CalculateId(_dbPath);
            item.Id = count;
            var jsonString = JsonSerializer.Serialize(item);
            var path = $"{_dbPath}\\{count}{_fileFormat}";

            if (File.Exists(path))
            {
                throw new ArgumentException($"File with id {count} already exists", path);
            }

            File.WriteAllText(path, jsonString);
        }

        public async Task<List<Item>> GetItems(int? categoryId = null, int? page = null)
        {
            var list = new List<Item>();
            var files = Directory.GetFiles(_dbPath);

            foreach (var file in files)
            {
                var openedFile = await File.ReadAllTextAsync(file);
                var item = JsonSerializer.Deserialize<Item>(openedFile);
                if (item != null && (categoryId != null && item.CategoryId.Equals(categoryId) || categoryId == null))
                {
                    list.Add(item);
                }
            }

            return page != null ? list.Skip(page.Value * _pageSize).Take(_pageSize).ToList() : list;
        }

        public void DeleteItem(int id)
        {
            var path = $"{_dbPath}\\{id}{_fileFormat}";

            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Specified file not found", path);
            }

            File.Delete(path);
        }

        public void UpdateItem(Item item)
        {
            var jsonString = JsonSerializer.Serialize(item);
            var path = $"{_dbPath}\\{item.Id}{_fileFormat}";

            if (!File.Exists(path))
            {
                throw new FileNotFoundException("File not found", path);
            }

            File.WriteAllText(path, jsonString);
        }

        public async Task<Item> GetById(int id)
        {
            var path = $"{_dbPath}\\{id}{_fileFormat}";

            if (!File.Exists(path))
            {
                throw new ArgumentException("Specified file does not exist", path);
            }

            var openedFile = await File.ReadAllTextAsync(path);
            var item = JsonSerializer.Deserialize<Item>(openedFile);

            return item;
        }
    }
}
