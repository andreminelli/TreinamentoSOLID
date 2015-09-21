using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOLID.ETL
{
    public class CountingAccountLoading : IAccountLoading
    {
        private readonly IAccountLoading _main;

        public CountingAccountLoading(IAccountLoading main)
        {
            this._main = main;
        }

        public int Count { get; private set; }

        public void Add(AccountData data)
        {
            this._main.Add(data);
            Count++;
        }

        public void Commit()
        {
            this._main.Commit();

            Console.WriteLine("Registros inseridos: {0}", Count);
            Console.WriteLine("Pression qualquer tecla para finalizar.");
            Console.ReadKey();
        }

        public void Rollback()
        {
            this._main.Rollback();
        }

        public void Dispose()
        {
            this._main.Dispose();
        }
    }
}
