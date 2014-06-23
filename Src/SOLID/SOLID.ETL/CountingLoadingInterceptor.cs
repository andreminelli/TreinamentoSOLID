using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.ETL
{
    public class CountingLoadingInterceptor : IInterceptor
    {
        public int Count { get; private set; }
        public void Intercept(IInvocation invocation)
        {
            invocation.Proceed();
            if (invocation.GetConcreteMethod().Name == "Add")
            {
                Count++;
            } 
            else if (invocation.GetConcreteMethod().Name == "Commit")
            {
                Console.WriteLine("Registros inseridos: {0}", Count);
                Console.WriteLine("Pression qualquer tecla para finalizar.");
                Console.ReadKey();
            }
        }
    }
}
