using ArMapAdmin.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Security.Cryptography;

namespace ArMapAdmin.Services
{
    public class ModelsService
    {
        private readonly IMongoCollection<Model> _booksCollection;

        public ModelsService(
            IOptions<ModelsDatabaseSettings> modelDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                modelDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                modelDatabaseSettings.Value.DatabaseName);

            _booksCollection = mongoDatabase.GetCollection<Model>(
                modelDatabaseSettings.Value.ModelsCollectionName);
        }

        public async Task<List<Model>> GetAsync() =>
            await _booksCollection.Find(_ => true).ToListAsync();

        public async Task<Model?> GetAsync(string id) => 
            await _booksCollection.Find(x => x._id.ToString() == id).FirstOrDefaultAsync();
        

        public async Task CreateAsync(Model newBook) =>
            await _booksCollection.InsertOneAsync(newBook);

        public async Task UpdateAsync(string id, Model updatedModel) =>
            await _booksCollection.ReplaceOneAsync(x => x._id.ToString() == id, updatedModel);

        public async Task RemoveAsync(string id) =>
            await _booksCollection.DeleteOneAsync(x => x._id.ToString() == id);
    }
}
