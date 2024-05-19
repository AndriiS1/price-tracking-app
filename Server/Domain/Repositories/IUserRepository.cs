using Domain.Models;
using MongoDB.Bson;

namespace Domain.Repositories;

public interface IUserRepository
{
    Task<User?> UpdateUserRefreshTokenData(ObjectId userId, string refreshToken, DateTime refreshTokenExpiryTime);
    Task<User?> Get(string password, string email);
    Task<User?> GetById(ObjectId id);
    Task<User?> GetByEmail(string email);
    Task Create(User user);
}