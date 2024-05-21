using Domain.Models;
using MongoDB.Bson;

namespace Domain.Repositories;

public interface IStoreRepository
{
    Task<List<Store>> GetAll();
    Task<List<Store>> GetAllRelated(IEnumerable<ObjectId> storesIds);
}