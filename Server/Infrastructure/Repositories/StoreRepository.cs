using Domain.Models;
using Domain.Repositories;
using Infrastructure.Database;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Repositories;

public class StoreRepository(MongoContext mongoContext) : IStoreRepository
{
    private readonly IMongoCollection<Store> _collection = mongoContext.Stores;

    public async Task<List<Store>> GetAll()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }

    public async Task<List<Store>> GetAllRelated(IEnumerable<ObjectId> storesIds)
    {
        var filter = Builders<Store>.Filter.In(store => store.Id, storesIds);
        return await _collection.Find(filter).ToListAsync();
    }
}