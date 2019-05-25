using DP.V2.Core.Common.Base;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DP.V2.Core.Data.Interface
{
    /// <summary>
    /// Repository interface
    /// </summary>
    /// <typeparam name="TEntity">Entity model</typeparam>
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        TEntity FindById(Guid id);
        TEntity FindOne(Expression<Func<TEntity, bool>> predicate);
        bool FindExist(Expression<Func<TEntity, bool>> predicate);
        int Count();
        IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> GetAll();
        TEntity Insert(TEntity entity);
        TEntity Update(TEntity entity);
        bool Remove(Guid id, bool islogic = true);
        void BulkRemove(Expression<Func<TEntity, bool>> predicate, bool islogic = true);
        void BulkUpdate(TEntity[] entity);
        void BulkInsert(TEntity[] entity);
        IEnumerable<TResult> ExcuteStoredProcedure<TResult>(string nameofStored, object model) where TResult : class;
        IEnumerable<TResult> ExcuteStoredProcedure<TResult>(string nameofStored, params NpgsqlParameter[] parameters) where TResult : class;
        IEnumerable<TResult> ExecStoreReturnJson<TResult>(string nameofStored, object model) where TResult : class;
        IEnumerable<TResult> ExecStoreReturnJson<TResult>(string nameofStored, params NpgsqlParameter[] parameters) where TResult : class;
    }
}
