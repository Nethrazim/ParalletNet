using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ParalletNet.Basic
{
    internal class HandlingBasicExceptions
    {
        public static void Run()
        {
            Task task1 = new Task(() => { 
                ArgumentOutOfRangeException exception = new ArgumentOutOfRangeException();
                exception.Source = "task1";
                throw exception;
            });

            Task task2 = new Task(() => { 
                throw new NullReferenceException();
            });

            Task task3 = new Task(() =>
            {
                Console.WriteLine("Hello from Task 3");
            });

            task1.Start(); task2.Start(); task3.Start();

            try
            {
                Task.WaitAll(task1, task2, task3);
            }
            catch (AggregateException ex) {
                foreach (Exception inner in ex.InnerExceptions) {
                    Console.WriteLine("Exception type {0} from {1}", inner.GetType(), inner.Source);
                }
            }
        }
    }
}
