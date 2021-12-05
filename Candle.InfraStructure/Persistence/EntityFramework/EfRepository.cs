using Candle.Model.Common;
using Candle.Model.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Candle.InfraStructure.Persistence.EntityFramework
{
    public class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly DbContext dbContext;

        public EfRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        protected virtual DbSet<T> Entities => dbContext.Set<T>();

        public bool Any(Expression<Func<T, bool>> expression)
        {
            return Entities.Any(expression);
        }

        public int Commit()
        {
            try
            {
                return dbContext.SaveChanges();
            }
            catch (DbUpdateException exception)
            {
                string message = ErrorTextAndRollback(exception);
                throw new Exception(message);
            }
        }

        #region Get

        public T Get(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
        {
            var query = Entities.Where(expression);
            return includes.Aggregate(query, (currrent, includeProperty) => currrent.Include(includeProperty)).FirstOrDefault();
        }

        public IQueryable<T> GetAll()
        {
            return Entities;
        }

        public IQueryable<T> GetAllNoTracking()
        {
            return Entities.AsNoTracking();
        }

        public T GetbyId(Guid Id)
        {
            return Entities.Find(Id);
        }

        public IQueryable<T> GetMany(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
        {
            var query = Entities.Where(expression);
            return includes.Aggregate(query, (currrent, includeProperty) => currrent.Include(includeProperty));
        }

        #endregion

        #region Insert

        public T InsertWithoutCommit(T entity)
        {
            if (entity == null)
                throw new Exception(nameof(entity));

            Entities.Add(entity);
            return entity;
        }

        public void Insert(T entity)
        {
            if (entity == null)
                throw new Exception(nameof(entity));

            Entities.Add(entity);
            Commit();
        }

        public void InsertBulk(IEnumerable<T> entities)
        {
            if (entities == null)
                throw new Exception(nameof(entities));

            Entities.AddRange(entities);
            Commit();
        }

        #endregion

        #region Update

        public void Update(T entity)
        {
            if (entity == null)
                throw new Exception(nameof(entity));

            Entities.Update(entity);
            Commit();
        }

        public void UpdateWithoutCommit(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                Entities.Update(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

        }

        #endregion

        #region Delete

        public void DeleteWithoutCommit(T entity)
        {
            if (entity == null)
                throw new Exception(nameof(entity));

            try
            {
                Entities.Remove(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public void HardDelete(T entity)
        {
            if (entity == null)
                throw new Exception(nameof(entity));

            Entities.Remove(entity);
            Commit();
        }

        public void Delete(T entity)
        {
            if (entity == null)
                throw new Exception(nameof(entity));

            dbContext.Entry(entity).State = EntityState.Modified;
            (dbContext.Entry(entity).Entity).IsActive = 0;
            Commit();
        }   

        public void DeleteBulk(IEnumerable<T> entities)
        {
            if (entities == null)
                throw new Exception(nameof(entities));

            Entities.RemoveRange(entities);
            Commit();
        }

        
        #endregion

        #region Methods
        protected string ErrorTextAndRollback(DbUpdateException dbUpdateException)
        {
            if (dbContext is DbContext _dbContext)
            {
                var entries = _dbContext.ChangeTracker.Entries()
                                 .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified).ToList();

                entries.ForEach(entry => {
                    try
                    {
                        entry.State = EntityState.Unchanged;
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                });
            }

            try
            {
                dbContext.SaveChanges();
                return dbUpdateException.ToString();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        #endregion

    }
}
