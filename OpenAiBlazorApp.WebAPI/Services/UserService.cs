using MongoDB.Bson;
using MongoDB.Driver;
using OpenAiBlazorApp.WebAPI.Models;

namespace OpenAiBlazorApp.WebAPI.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _users;

        public UserService(IPerFinsDatabaseSettings settings)
        {
            if (string.IsNullOrEmpty(settings.ConnectionString) ||
                string.IsNullOrEmpty(settings.DatabaseName) ||
                string.IsNullOrEmpty(settings.UsersCollectionName))
            {
                throw new ArgumentException("Database settings must be properly initialized.");
            }

            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _users = database.GetCollection<User>(settings.UsersCollectionName);
        }

        public List<User> Get() =>
            _users.Find(user => true).ToList();

        public User Get(string id) =>
            _users.Find<User>(user => user.Id == id).FirstOrDefault();

        public User Create(User user)
        {
            user.Id = ObjectId.GenerateNewId().ToString();
            _users.InsertOne(user);
            return user;
        }

        public void Update(string id, User userIn) =>
            _users.ReplaceOne(user => user.Id == id, userIn);

        public void Remove(User userIn) =>
            _users.DeleteOne(user => user.Id == userIn.Id);

        public void Remove(string id) =>
            _users.DeleteOne(user => user.Id == id);
    }
}




