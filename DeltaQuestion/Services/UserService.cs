using DeltaQuestion.Data;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Microsoft.Extensions.Options;

namespace DeltaQuestion.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _UserCollection;

        public UserService(
            IOptions<DatabaseSettings> DatabaseSettings)
        {
            var mongoClient = new MongoClient(
               DatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                DatabaseSettings.Value.DatabaseName);

            _UserCollection = mongoDatabase.GetCollection<User>(
               DatabaseSettings.Value.BooksCollectionName);
        }

        public async Task<List<User>> GetAsync()
        {
            var res = await _UserCollection.Find(_ => true).ToListAsync();
            return res;
        }

        public async Task<User?> GetAsync(int id) =>
            await _UserCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        public async Task<User?> GetAsync(string username, string password) =>
            await _UserCollection.Find(x => x.UserName == username && x.password==password).FirstOrDefaultAsync();

        public async Task CreateAsync(User newUser) =>
            await _UserCollection.InsertOneAsync(newUser);

        public async Task UpdateAsync(int id, User updatedUser) =>
            await _UserCollection.ReplaceOneAsync(x => x.Id == id, updatedUser);
        
        public async Task RemoveAsync(int id) =>
            await _UserCollection.DeleteOneAsync(x => x.Id == id);
    }
}
