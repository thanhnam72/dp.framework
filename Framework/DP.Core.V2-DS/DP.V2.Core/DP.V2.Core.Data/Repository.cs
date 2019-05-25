using DP.V2.Core.Common.Base;
using DP.V2.Core.Common.Entities;
using DP.V2.Core.Common.Extensions;
using DP.V2.Core.Data.Interface;
using DP.V2.Core.Data.UoW;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace DP.V2.Core.Data
{
    /// <summary>
    /// Repository class
    /// </summary>
    /// <typeparam name="TEntity">Entity model</typeparam>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly DbSet<TEntity> _dbSet;
        private readonly IUnitOfWork _unitOfWork;
        private readonly DbContext _context;

        public Repository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _context = _unitOfWork.GetContext();
            _dbSet = _context.Set<TEntity>();
        }

        /// <summary>
        /// Tìm theo Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity FindById(Guid id)
        {
            try
            {
                return _dbSet.Find(id);
            }
            catch
            {
                return default(TEntity);
            }
        }

        /// <summary>
        /// Tìm 1 record theo điều kiện truyền vào là 1 x => x.a
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public TEntity FindOne(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                return _dbSet.FirstOrDefault(predicate);
            }
            catch
            {
                return default(TEntity);
            }
        }

        /// <summary>
        /// Tìm kiếm xem có tồn tại dữ liệu theo điều kiện truyền vào ko
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public bool FindExist(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Any(predicate);
        }

        /// <summary>
        /// Hàm đếm số record
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return _dbSet.Count();
        }

        /// <summary>
        /// Tìm tất cả các record thỏa điều kiện truyền vào
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        /// <summary>
        /// Lấy tất cả dữ liệu
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> GetAll()
        {
            return _dbSet.AsQueryable();
        }

        /// <summary>
        /// Insert dữ liệu vào bảng
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public TEntity Insert(TEntity entity)
        {
            _dbSet.Add(entity);

            _unitOfWork.Commit(MethodBase.GetCurrentMethod().Name, entity);

            return entity;
        }

        /// <summary>
        /// Update dữ liệu 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public TEntity Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;

            _dbSet.Update(entity);

            _unitOfWork.Commit(MethodBase.GetCurrentMethod().Name, entity);

            return entity;
        }

        /// <summary>
        /// Update nhiều dòng
        /// </summary>
        /// <param name="entities"></param>
        public void BulkUpdate(TEntity[] entities)
        {
            foreach(TEntity entity in entities)
            {
                _context.Entry(entity).State = EntityState.Modified;
            }

            _dbSet.UpdateRange(entities);

            _unitOfWork.Commit(MethodBase.GetCurrentMethod().Name, entities);
        }

        /// <summary>
        /// Thêm nhiều dòng
        /// </summary>
        /// <param name="entities"></param>
        public void BulkInsert(TEntity[] entities)
        {
            _dbSet.AddRange(entities);

            _unitOfWork.Commit(MethodBase.GetCurrentMethod().Name, entities);
        }

        /// <summary>
        /// Xóa dòng theo biến islogic = true là xóa logic, nguoc lai xoa khoi db
        /// </summary>
        /// <param name="id"></param>
        /// <param name="islogic"></param>
        /// <returns></returns>
        public bool Remove(Guid id, bool islogic = true)
        {
            var entity = _dbSet.Find(id);

            if (islogic)
            {
                entity.Deleted = true;
                _context.Entry(entity).State = EntityState.Modified;
                _dbSet.Update(entity);
            }
            else
            {
                _dbSet.Remove(entity);
            }

            _unitOfWork.Commit(MethodBase.GetCurrentMethod().Name, id);

            return true;
        }

        /// <summary>
        /// Xóa nhiều dòng
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="islogic"></param>
        public void BulkRemove(Expression<Func<TEntity, bool>> predicate, bool islogic = true)
        {
            var entities = _dbSet.Where(predicate).AsEnumerable();

            if (islogic)
            {
                foreach(var entity in entities)
                {
                    entity.Deleted = true;
                    _context.Entry(entity).State = EntityState.Modified;
                }

                _dbSet.UpdateRange(entities);
            }
            else
            {
                _dbSet.RemoveRange(entities);
            }

            _unitOfWork.Commit(MethodBase.GetCurrentMethod().Name, entities);
        }


        /// <summary>
        /// Gọi store tu database
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="nameofStored"></param>
        /// <param name="model"></param>
        /// <returns>IEnumerable<TResult></returns>
        public IEnumerable<TResult> ExcuteStoredProcedure<TResult>(string nameofStored, object model) where TResult : class
        {
            List<NpgsqlParameter> parameters = new List<NpgsqlParameter>();

            var context = _unitOfWork.GetContext();

            if (context.Database.GetDbConnection().State != ConnectionState.Open)
                context.Database.OpenConnection();

            using (var command = context.Database.GetDbConnection().CreateCommand())
            {
                foreach (var prop in model.GetType().GetProperties())
                {
                    parameters.Add(new NpgsqlParameter(prop.Name, prop.GetValue(model)));
                }
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = nameofStored;
                command.Parameters.AddRange((Array)parameters.ToArray());

                DataTable tb = new DataTable();

                using (var dr = command.ExecuteReader())
                    tb.Load(dr);

                _unitOfWork.WriteLog("Method: ExcuteStoredProcedure. Store: "+ nameofStored + ". Input: " + JsonConvert.SerializeObject(model));

                return tb.ToList<TResult>();
            }
        }

        /// <summary>
        /// Gọi store từ database
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="nameofStored"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IEnumerable<TResult> ExcuteStoredProcedure<TResult>(string nameofStored, params NpgsqlParameter[] parameters) where TResult : class
        {
            var context = _unitOfWork.GetContext();
            if (context.Database.GetDbConnection().State != ConnectionState.Open)
                context.Database.OpenConnection();

            using (var command = context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = nameofStored;
                command.Parameters.AddRange(parameters);

                DataTable tb = new DataTable();

                using (var dr = command.ExecuteReader())
                    tb.Load(dr);

                _unitOfWork.WriteLog("Method: ExcuteStoredProcedure. Store: " + nameofStored + ". Input: " + JsonConvert.SerializeObject(parameters.Select(x => new { x.ParameterName, x.Value })));

                return tb.ToList<TResult>();
            }
        }

        /// <summary>
        /// Gọi store từ database mà trả về json
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="nameofStored"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public IEnumerable<TResult> ExecStoreReturnJson<TResult>(string nameofStored, object model) where TResult : class
        {
            var result = ExcuteStoredProcedure<JsonRecord>(nameofStored, model).ToList();

            return result.Select(x => JsonConvert.DeserializeObject<TResult>(x.to_json));
        }

        /// <summary>
        /// Gọi store từ database mà trả về json
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="nameofStored"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IEnumerable<TResult> ExecStoreReturnJson<TResult>(string nameofStored, params NpgsqlParameter[] parameters) where TResult : class
        {
            var result = ExcuteStoredProcedure<JsonRecord>(nameofStored, parameters).ToList();

            return result.Select(x => JsonConvert.DeserializeObject<TResult>(x.to_json));
        }
    }
}

