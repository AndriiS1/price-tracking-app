using System.Linq.Expressions;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly DbContext Context;

    public Repository(DbContext context)
    {
        Context = context;
    }

    public void Add(TEntity entity)
    {
        Context.Add(entity);
    }

    public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
    {
        return Context.Set<TEntity>().Where(predicate).ToList();
    }

    public IEnumerable<bool> Select(Expression<Func<TEntity, bool>> predicate)
    {
        return Context.Set<TEntity>().Select(predicate);
    }


    public TEntity? Get(int id)
    {
        return Context.Set<TEntity>().Find(id);
    }

    public TEntity? Single(Expression<Func<TEntity, bool>> predicate)
    {
        return Context.Set<TEntity>().Single(predicate);
    }

    public TEntity? SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
    {
        return Context.Set<TEntity>().SingleOrDefault(predicate);
    }

    public TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
    {
        return Context.Set<TEntity>().FirstOrDefault(predicate);
    }

    public IEnumerable<TEntity> GetAll()
    {
        return Context.Set<TEntity>().ToList();
    }

    public void Remove(TEntity entity)
    {
        Context.Remove(entity);
    }
}