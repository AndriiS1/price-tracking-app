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

    public MongoContext(IOptions<ConnectionStrings> connectionString)
    {
        var client = new MongoClient(connectionString.Value.Database);
        _database = client.GetDatabase(Database);
    }

    public IMongoCollection<User> Users => _database.GetCollection<User>(UserCollection);
}