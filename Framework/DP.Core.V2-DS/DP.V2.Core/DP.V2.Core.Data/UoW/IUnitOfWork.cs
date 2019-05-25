using Microsoft.EntityFrameworkCore;

namespace DP.V2.Core.Data.UoW
{
    public interface IUnitOfWork
    {
        DbContext GetContext();
        void WriteLog(string desc);
        int Commit();
        int Commit(string method, object data);
    }
}
