using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;

namespace ParalletNet.Basic
{
    internal class GettingResult
    {
        public static void Run()
        {
            Task<int> task1 = new Task<int>(() => {
                int sum = 0;

                for (int i = 0; i < 100; i++)
                {
                    sum += i;
                }

                return sum;
            });

            task1.Start();

            Console.WriteLine("Result 1: {0}",task1.Result);


            Task<int> task2 = new Task<int>(obj =>
            {
                int sum = 0;
                int max = (int)obj;
                for (int i = 0; i < max; i++)
                {
                    sum += i;
                }
                return sum;
            }, 100);

            task2.Start();

            Console.WriteLine("Result 2: {0}", task2.Result);


            Task<int> task3 = Task.Factory.StartNew<int>(() =>
            {
                return 100;
            });
        }
    }
}
