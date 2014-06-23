using System;
namespace SOLID.ETL
{
    public interface IAccountExtractor: IDisposable
    {
        AccountData GetNext();
    }
}
