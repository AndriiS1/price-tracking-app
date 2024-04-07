﻿using Domain.Dto;
using Domain.Models;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(DbContext context) : base(context)
    {
    }

    public void UpdateUserRefreshTokenData(Guid userId, RefreshTokenDataDto refreshTokenDataDto)
    {
        var userToUpdate = Context.Set<User>().Single(u => u.Id == userId);
        userToUpdate.RefreshToken = refreshTokenDataDto.RefreshToken;
        userToUpdate.RefreshTokenExpiryTime = refreshTokenDataDto.RefreshTokenExpiryTime;
    }
}