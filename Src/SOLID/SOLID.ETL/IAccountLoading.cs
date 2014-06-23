using System;
namespace SOLID.ETL
{
    public interface IAccountLoading : IDisposable
    {
        void Add(AccountData data);
        void Commit();
        void Rollback();
    }
}
