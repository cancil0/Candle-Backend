using Candle.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Candle.InfraStructure.Persistence
{
    public interface IRepository<T> where T : BaseEntity
    {
        T GetbyId(Guid id);
        IQueryable<T> GetAll();
        IQueryable<T> GetAllNoTracking();
        T Get(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes);
        IQueryable<T> GetMany(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes);
        T InsertWithoutCommit(T entity);
        void Insert(T entity);
        void InsertBulk(IEnumerable<T> entities);
        void Update(T entity);
        void UpdateWithoutCommit(T entity);
        void Delete(T entity);
        void DeleteWithoutCommit(T entity);
        void HardDelete(T entity);
        void DeleteBulk(IEnumerable<T> entities);
        bool Any(Expression<Func<T, bool>> expression);
        int Commit();
    }
}
