using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOLID.ETL
{
    public class CountingAccountLoading : IAccountLoading
    {
        private readonly IAccountLoading _main;

        private int count;

        public CountingAccountLoading(IAccountLoading main)
        {
            this._main = main;
        }


        public void Add(AccountData data)
        {
            this._main.Add(data);
            count++;
        }

        public void Commit()
        {
            this._main.Commit();

            Console.WriteLine("Registros inseridos: {0}", count);
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
