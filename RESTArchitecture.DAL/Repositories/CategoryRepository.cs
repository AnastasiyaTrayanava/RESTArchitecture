using System.Text.Json;
using RESTArchitecture.Common.Interfaces;
using RESTArchitecture.Common.Models;
using RESTArchitecture.Common.Utils;

namespace RESTArchitecture.DAL.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private const string _dbPath = @"C:\RESTDB\Categories";
        private const string _fileFormat = ".json";
        private readonly ItemRepository _itemRepository;

        public CategoryRepository()
        {
            _itemRepository = new ItemRepository();
        }

        public void AddCategory(Category category)
        {
            var count = Calculations.CalculateId(_dbPath); 
            category.Id = count;

            foreach (var item in category.ItemsIds)
            {
                var filePath = $"{_dbPath}\\{item}{_fileFormat}";
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException("Specified file not found", filePath);
                }
            }

            var jsonString = JsonSerializer.Serialize(category);
            var path = $"{_dbPath}\\{count}{_fileFormat}";

            if (File.Exists(path))
            {
                throw new ArgumentException($"File with id {count} already exists", path);
            }

            File.WriteAllText(path, jsonString);
        }

        public void UpdateCategory(Category category)
        {
            var jsonString = JsonSerializer.Serialize(category);
            var path = $"{_dbPath}\\{category.Id}{_fileFormat}";

            if (!File.Exists(path))
            {
                throw new FileNotFoundException("File not found", path);
            }

            File.WriteAllText(path, jsonString);
        }

        public async Task<List<Category>> GetListOfCategories(CancellationToken token)
        {
            var list = new List<Category>();
            var files = Directory.GetFiles(_dbPath);

            await Parallel.ForEachAsync(files, async (s, token) =>
            {
                var openedFile = await File.ReadAllTextAsync(s, token);
                var category = JsonSerializer.Deserialize<Category>(openedFile);
                if (category != null)
                {
                    list.Add(category);
                }
            });

            return list;
        }

        public void DeleteById(int id)
        {
            var path = $"{_dbPath}\\{id}{_fileFormat}";

            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Specified file not found", path);
            }

            File.Delete(path);
        }
    }
}
