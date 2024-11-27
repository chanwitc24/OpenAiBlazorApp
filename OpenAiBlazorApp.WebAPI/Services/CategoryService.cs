using MongoDB.Bson;
using MongoDB.Driver;
using OpenAiBlazorApp.WebAPI.Models;

namespace OpenAiBlazorApp.WebAPI.Services
{
    public class CategoryService
    {
        private readonly IMongoCollection<Category> _category;

        public CategoryService(IPerFinsDatabaseSettings settings)
        {
            if (string.IsNullOrEmpty(settings.ConnectionString) ||
                string.IsNullOrEmpty(settings.DatabaseName) ||
                string.IsNullOrEmpty(settings.CategoriesCollectionName))
            {
                throw new ArgumentException("Database settings must be properly initialized.");
            }

            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _category = database.GetCollection<Category>(settings.CategoriesCollectionName);
        }

        public List<Category> Get() =>
            _category.Find(category => true).ToList();

        public Category Get(string id) =>
            _category.Find<Category>(category => category.Id == id).FirstOrDefault();

        public Category Create(Category category)
        {
            category.Id = ObjectId.GenerateNewId().ToString();
            _category.InsertOne(category);
            return category;
        }

        public void Update(string id, Category categoryIn) =>
            _category.ReplaceOne(category => category.Id == id, categoryIn);

        public void Remove(Category categoryIn) =>
            _category.DeleteOne(category => category.Id == categoryIn.Id);

        public void Remove(string id) =>
            _category.DeleteOne(category => category.Id == id);
    }
}



