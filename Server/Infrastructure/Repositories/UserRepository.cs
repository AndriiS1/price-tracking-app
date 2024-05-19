using Domain.Models;
using Domain.Repositories;
using Infrastructure.Database;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Repositories;

public class UserRepository(MongoContext mongoContext) : IUserRepository
{
    private readonly IMongoCollection<User> _collection = mongoContext.Users;

    public async Task Create(User user)
    {
        await _collection.InsertOneAsync(user);
    }

    public async Task<User?> Get(string password, string email)
    {
        var filter = Builders<User>.Filter.Where(u => u.Email == email && u.Password == password);
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<User?> GetByEmail(string email)
    {
        var filter = Builders<User>.Filter.Where(u => u.Email == email);
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<User?> GetById(ObjectId id)
    {
        var filter = Builders<User>.Filter.Where(u => u.Id == id);
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<User?> UpdateUserRefreshTokenData(ObjectId userId, string refreshToken,
        DateTime refreshTokenExpiryTime)
    {
        var filter = Builders<User>.Filter.Eq(u => u.Id, userId);
        var update = Builders<User>.Update
            .Set(u => u.RefreshToken, refreshToken)
            .Set(u => u.RefreshTokenExpiryTime, refreshTokenExpiryTime);

        return await _collection.FindOneAndUpdateAsync(filter, update);
    }
}