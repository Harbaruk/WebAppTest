using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace WebTestApp.DAL.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Set { get; }
        T Insert(T item);
        void Delete(T item);
        IQueryable<T> Include(params Expression<Func<T, object>>[] include);
    }
}
