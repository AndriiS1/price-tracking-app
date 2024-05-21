using Domain.Models;
using MongoDB.Bson;

namespace Domain.Repositories;

public interface ITrackedProductRepository
{
    Task<TrackedProduct?> GetByName(string name);
    Task Create(TrackedProduct product);
    Task Increment(ObjectId id, int increment);
    Task<List<TrackedProduct>> Get(int page, int size);
    Task<TrackedProduct?> Get(ObjectId id);
}