using MongoDB.Bson;
using MongoDB.Driver;
using OpenAiBlazorApp.WebAPI.Models;

namespace OpenAiBlazorApp.WebAPI.Services
{
    public class MonthlyAmountService
    {
        private readonly IMongoCollection<MonthlyAmount> _monthlyAmounts;

        public MonthlyAmountService(IPerFinsDatabaseSettings settings)
        {
            if (string.IsNullOrEmpty(settings.ConnectionString) ||
                string.IsNullOrEmpty(settings.DatabaseName) ||
                string.IsNullOrEmpty(settings.MonthlyAmountsCollectionName))
            {
                throw new ArgumentException("Database settings must be properly initialized.");
            }

            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _monthlyAmounts = database.GetCollection<MonthlyAmount>(settings.MonthlyAmountsCollectionName);
        }

        public List<MonthlyAmount> Get() =>
            _monthlyAmounts.Find(monthlyAmount => true).ToList();

        public MonthlyAmount Get(string id) =>
            _monthlyAmounts.Find<MonthlyAmount>(monthlyAmount => monthlyAmount.Id == id).FirstOrDefault();

        public MonthlyAmount Create(MonthlyAmount monthlyAmount)
        {
            monthlyAmount.Id = ObjectId.GenerateNewId().ToString();
            _monthlyAmounts.InsertOne(monthlyAmount);
            return monthlyAmount;
        }

        public void Update(string id, MonthlyAmount monthlyAmountIn) =>
            _monthlyAmounts.ReplaceOne(monthlyAmount => monthlyAmount.Id == id, monthlyAmountIn);

        public void Remove(MonthlyAmount monthlyAmountIn) =>
            _monthlyAmounts.DeleteOne(monthlyAmount => monthlyAmount.Id == monthlyAmountIn.Id);

        public void Remove(string id) =>
            _monthlyAmounts.DeleteOne(monthlyAmount => monthlyAmount.Id == id);
    }
}





