using Domain.Models;
using Domain.Repositories;
using Infrastructure.Database;
using MongoDB.Driver;

namespace Infrastructure.Repositories;

public class ProductStatisticRepository(MongoContext mongoContext) : IProductStatisticRepository
{
    private readonly IMongoCollection<ProductStatistic> _collection = mongoContext.ProductStatistics;

    public async Task CreateMany(IEnumerable<ProductStatistic> statistics)
    {
        await _collection.InsertManyAsync(statistics);
    }
}