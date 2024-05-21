using Domain.Models;

namespace Domain.Repositories;

public interface IProductStatisticRepository
{
    Task CreateMany(IEnumerable<ProductStatistic> statistics);
}