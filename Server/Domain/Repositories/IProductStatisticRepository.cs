using Domain.Models;
using MongoDB.Bson;

namespace Domain.Repositories;

public interface IProductStatisticRepository
{
    Task CreateMany(IEnumerable<ProductStatistic> statistics);
    Task<List<ProductStatistic>> GetLatest(IEnumerable<ObjectId> trackedObjectIds);
}