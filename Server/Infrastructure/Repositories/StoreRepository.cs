using Domain.Models;
using Domain.Repositories;
using Infrastructure.Database;
using MongoDB.Driver;

namespace Infrastructure.Repositories;

public class StoreRepository(MongoContext mongoContext) : IStoreRepository
{
    private readonly IMongoCollection<Store> _collection = mongoContext.Stores;

    public async Task<List<Store>> GetAll()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }
}