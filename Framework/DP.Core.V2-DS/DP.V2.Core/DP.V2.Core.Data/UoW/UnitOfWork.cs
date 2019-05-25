using DP.V2.Core.Data.DataModel;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;

namespace DP.V2.Core.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;
        private readonly IHttpContextAccessor _httpContext;

        public UnitOfWork(DataContext context, IHttpContextAccessor httpContext)
        {
            _dbContext = context;
            _httpContext = httpContext;
        }

        public int Commit()
        {
            return _dbContext.SaveChanges();
        }

        public int Commit(string method, object data)
        {
            string log = string.Format("Method: {0}. Input: {1}", method, JsonConvert.SerializeObject(data));
            WriteLog(log);
            return Commit();
        }

        public DbContext GetContext()
        {
            return _dbContext;
        }

        public void WriteLog(string desc)
        {
            var user = ((SysUser)_httpContext.HttpContext.Items["UserContext"]);
            string controller = _httpContext.HttpContext.Items["ControllerName"].ToString();
            string action = _httpContext.HttpContext.Items["ActionName"].ToString();
            string createby = user != null ? user.Username : "anonymous";
            string sqlCmnd = "INSERT INTO \"SysLog\" SELECT {0}, {1}, {2}, {3}, now(), now(), {4}, 0, FALSE, TRUE";
            _dbContext.Database.ExecuteSqlCommand(sqlCmnd, Guid.NewGuid(), controller, action, desc, createby);
        }
    }
}
