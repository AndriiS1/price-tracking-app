using Domain.Models;
using Domain.Repositories;
using Infrastructure.Database;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Repositories;

public class TrackedProductRepository(MongoContext mongoContext) : ITrackedProductRepository
{
    private readonly IMongoCollection<TrackedProduct> _collection = mongoContext.TrackedProducts;

    public async Task Create(TrackedProduct product)
    {
        await _collection.InsertOneAsync(product);
    }

    public async Task<List<TrackedProduct>> Get(int page, int size)
    {
        var skip = (page - 1) * size;
        var sortDefinition = Builders<TrackedProduct>.Sort.Descending(product => product.TotalSearchCount);
        return await _collection.Find(product => true)
            .Sort(sortDefinition)
            .Skip(skip)
            .Limit(size)
            .ToListAsync();
    }

    public async Task<TrackedProduct?> Get(ObjectId id)
    {
        var filter = Builders<TrackedProduct>.Filter.Eq(p => p.Id, id);
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<TrackedProduct?> GetByName(string name)
    {
        var filter = Builders<TrackedProduct>.Filter.Eq(p => p.Name, name);
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task Increment(ObjectId id, int increment)
    {
        var filter = Builders<TrackedProduct>.Filter.Eq(p => p.Id, id);
        var update = Builders<TrackedProduct>.Update.Inc(p => p.TotalSearchCount, increment);

        await _collection.UpdateOneAsync(filter, update);
    }
}