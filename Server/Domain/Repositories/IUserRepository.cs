using Domain.Dto;
using Domain.Models;

namespace Domain.Repositories;

public interface IUserRepository : IRepository<User>
{
    void UpdateUserRefreshTokenData(Guid userId, RefreshTokenDataDto refreshTokenDataDto);
}