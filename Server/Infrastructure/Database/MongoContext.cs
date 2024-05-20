using Domain.Models;
using Infrastructure.Options;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Database;

public class MongoContext
{
    private readonly IMongoDatabase _database;
    private const string Database = "PriceSpaceDatabase";
    private const string UserCollection = "users";
    private const string ProductStatisticsCollection = "product-statistics";
    private const string StoresCollection = "stores";
    private const string TrackedProductsCollections = "tracked-products";

    public MongoContext(IOptions<ConnectionStrings> connectionString)
    {
        var client = new MongoClient(connectionString.Value.Database);
        _database = client.GetDatabase(Database);
    }

    public IMongoCollection<User> Users => _database.GetCollection<User>(UserCollection);

    public IMongoCollection<ProductStatistic> ProductStatistics =>
        _database.GetCollection<ProductStatistic>(ProductStatisticsCollection);

    public IMongoCollection<Store> Stores => _database.GetCollection<Store>(StoresCollection);

    public IMongoCollection<TrackedProduct> TrackedProducts =>
        _database.GetCollection<TrackedProduct>(TrackedProductsCollections);
}