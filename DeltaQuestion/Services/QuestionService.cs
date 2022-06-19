using DeltaQuestion.Data;
using DeltaQuestion.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DeltaQuestion.Services
{
    public class QuestionService
    {
        private readonly IMongoCollection<Question> _questionCollection;
        private readonly IMongoCollection<Comment> _commentCollection;

        public QuestionService()
        {
            var mongoClient = new MongoClient(
                "mongodb://localhost:27017"); 
            var mongoCommentClient = new MongoClient(
                "mongodb://localhost:27017");

            var mongoQuestionDatabase = mongoClient.GetDatabase(
                "Delta");
            var mongoCommentsDatabase = mongoCommentClient.GetDatabase(
                "Delta");

            _questionCollection = mongoQuestionDatabase.GetCollection<Question>(
                "Question");
            _commentCollection = mongoCommentsDatabase.GetCollection<Comment>(
                "Comment");
        }

        public async Task<List<Question>> GetAsync() =>
            await _questionCollection.Find(_ => true).ToListAsync();

        public async Task<Question?> GetAsync(int id) =>
            await _questionCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<List<Question>> GetAsync(long userId) =>
            await _questionCollection.Find(x => x.UserId == userId).ToListAsync();

        public async Task CreateAsync(Question Question) =>
            await _questionCollection.InsertOneAsync(Question);

        public async Task UpdateAsync(int id, Question Question) =>
            await _questionCollection.ReplaceOneAsync(x => x.Id == id, Question);

        public async Task RemoveAsync(int id) =>
            await _questionCollection.DeleteOneAsync(x => x.Id == id);


        public async Task<DisplayQuestion> GetFullQuestionAsync(long questionId) => new()
        {
            Question = await _questionCollection.Find(x => x.Id == questionId).FirstOrDefaultAsync(),
            Comments = await _commentCollection.Find(x => x.QuestionId == questionId).ToListAsync()
        };
    }
}