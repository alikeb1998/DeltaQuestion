using DeltaQuestion.Data;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DeltaComment.Services;

public class CommentService
{
    private readonly IMongoCollection<Comment> _questionCollection;

    public CommentService()
    {
        var mongoClient = new MongoClient(
            "mongodb://localhost:27017");

        var mongoDatabase = mongoClient.GetDatabase(
            "Delta");

        _questionCollection = mongoDatabase.GetCollection<Comment>(
            "Comment");
    }

    public async Task<List<Comment>> GetAsync() =>
        await _questionCollection.Find(_ => true).ToListAsync();

    public async Task<Comment?> GetAsync(int id) =>
        await _questionCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task<List<Comment>> GetAsync(long questionId) =>
        await _questionCollection.Find(x => x.QuestionId == questionId).ToListAsync();
        
    public async Task CreateAsync(Comment Comment) =>
        await _questionCollection.InsertOneAsync(Comment);

    public async Task UpdateAsync(int id, Comment Comment) =>
        await _questionCollection.ReplaceOneAsync(x => x.Id == id, Comment);

    public async Task RemoveAsync(int id) =>
        await _questionCollection.DeleteOneAsync(x => x.Id == id);
}