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
        private readonly IItemRepository _itemRepository;

        public CategoryRepository()
        {
            _itemRepository = new ItemRepository();
        }

        public void AddCategory(Category category)
        {
            var count = Calculations.CalculateId(_dbPath); 
            category.Id = count;

            if (category.ItemsIds != null && category.ItemsIds.Any())
            {
                CheckIfIdsExist(category.ItemsIds);
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

            foreach (var file in files)
            {
                var openedFile = await File.ReadAllTextAsync(file, token);
                var category = JsonSerializer.Deserialize<Category>(openedFile);

                if (category == null) continue;
                if (category.ItemsIds != null)
                {
                    foreach (var id in category.ItemsIds)
                    {
                        var item = await _itemRepository.GetById(id);
                        category.Items ??= new List<Item>();
                        category.Items.Add(item);
                    }
                }

                list.Add(category);
            }

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

        private static void CheckIfIdsExist(List<int> ids)
        {
            foreach (var id in ids)
            {
                var filePath = $"{_dbPath}\\{id}{_fileFormat}";
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException("Specified file not found", filePath);
                }
            }
        }
    }
}
