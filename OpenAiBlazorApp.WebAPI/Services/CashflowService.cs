using MongoDB.Bson;
using MongoDB.Driver;
using OpenAiBlazorApp.WebAPI.Models;

namespace OpenAiBlazorApp.WebAPI.Services
{
    public class CashflowService
    {
        private readonly IMongoCollection<Cashflow> _cashflows;

        public CashflowService(IPerFinsDatabaseSettings settings)
        {
            if (string.IsNullOrEmpty(settings.ConnectionString) ||
                string.IsNullOrEmpty(settings.DatabaseName) ||
                string.IsNullOrEmpty(settings.CashflowsCollectionName))
            {
                throw new ArgumentException("Database settings must be properly initialized.");
            }

            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _cashflows = database.GetCollection<Cashflow>(settings.CashflowsCollectionName);
        }

        public List<Cashflow> Get() =>
            _cashflows.Find(cashflow => true).ToList();

        public Cashflow Get(string id) =>
            _cashflows.Find<Cashflow>(cashflow => cashflow.Id == id).FirstOrDefault();

        public Cashflow Create(Cashflow cashflow)
        {
            cashflow.Id = ObjectId.GenerateNewId().ToString();
            if (cashflow.MonthlyAmounts != null)
            {
                foreach (var monthlyAmount in cashflow.MonthlyAmounts)
                {
                    monthlyAmount.Id ??= ObjectId.GenerateNewId().ToString();
                }
            }
            _cashflows.InsertOne(cashflow);
            return cashflow;
        }

        public void Update(string id, Cashflow cashflowIn) =>
            _cashflows.ReplaceOne(cashflow => cashflow.Id == id, cashflowIn);

        public void Remove(Cashflow cashflowIn) =>
            _cashflows.DeleteOne(cashflow => cashflow.Id == cashflowIn.Id);

        public void Remove(string id) =>
            _cashflows.DeleteOne(cashflow => cashflow.Id == id);
    }
}




