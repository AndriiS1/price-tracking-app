using Domain.Models;
using Domain.Repositories;
using Infrastructure.Database;
using MongoDB.Driver;

namespace Infrastructure.Repositories;

public class TrackedProductRepository(MongoContext mongoContext) : ITrackedProductRepository
{
    private readonly IMongoCollection<TrackedProduct> _collection = mongoContext.TrackedProducts;
}