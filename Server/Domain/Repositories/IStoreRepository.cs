using Domain.Models;

namespace Domain.Repositories;

public interface IStoreRepository
{
    Task<List<Store>> GetAll();
}