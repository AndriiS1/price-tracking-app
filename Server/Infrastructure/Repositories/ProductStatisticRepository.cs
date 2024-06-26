﻿using Domain.Models;
using Domain.Repositories;
using Infrastructure.Database;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Repositories;

public class ProductStatisticRepository(MongoContext mongoContext) : IProductStatisticRepository
{
    private readonly IMongoCollection<ProductStatistic> _collection = mongoContext.ProductStatistics;

    public async Task CreateMany(IEnumerable<ProductStatistic> statistics)
    {
        await _collection.InsertManyAsync(statistics);
    }

    public async Task<List<ProductStatistic>> GetAll(ObjectId productId)
    {
        var filter = Builders<ProductStatistic>.Filter.Where(p => p.TrackedProductId == productId);
        return await _collection.Find(filter).ToListAsync();
    }

    public async Task<List<ProductStatistic>> GetLatest(IEnumerable<ObjectId> trackedObjectIds)
    {
        var filter = Builders<ProductStatistic>.Filter.In(statistic => statistic.TrackedProductId, trackedObjectIds);

        return await _collection.Find(filter).ToListAsync();
    }
}