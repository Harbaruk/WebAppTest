using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace WebTestApp.DAL.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _dbContext;
        private readonly DbSet<T> _entities;

        public Repository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _entities = _dbContext.Set<T>();
        }

        public IQueryable<T> Set { get { return _entities; } }

        public T Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _entities.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _entities.Remove(entity);
            _dbContext.SaveChanges();
        }

        public IQueryable<T> Include(params Expression<Func<T, object>>[] include)
        {
            return include.Aggregate((IQueryable<T>)_entities, (current, inc) => current.Include(inc));
        }
    }
}
