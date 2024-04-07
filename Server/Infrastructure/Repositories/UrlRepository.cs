﻿using Domain.Dto;
using Domain.Models;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UrlRepository : Repository<Url>, IUrlRepository
{
    public UrlRepository(DbContext context) : base(context)
    {
    }

    public IEnumerable<TableUrlDataDto> GetAllTableUrls()
    {
        return Context.Set<Url>().Select(e => new TableUrlDataDto
        {
            Id = e.Id,
            OriginalUrl = e.OriginalUrl,
            ShortUrl = e.ShortUrl
        });
    }


    public IEnumerable<TableUrlDataDto> GetAllTableUrlsWithDeleteCheck(long userId)
    {
        return Context.Set<Url>().Select(e => new TableUrlDataDto
        {
            Id = e.Id,
            OriginalUrl = e.OriginalUrl,
            ShortUrl = e.ShortUrl,
            CanDelete = (e.UserId == userId)
        });
    }

    public IEnumerable<TableUrlDataDto> GetAllAdminTableUrls()
    {
        return Context.Set<Url>().Select(e => new TableUrlDataDto
        {
            Id = e.Id,
            OriginalUrl = e.OriginalUrl,
            ShortUrl = e.ShortUrl,
            CanDelete = true
        });
    }


    public Url? GetUrlWithLoadedUserData(long id)
    {
        return Context.Set<Url>().Include(e => e.User).SingleOrDefault(e => e.Id == id);
    }
}