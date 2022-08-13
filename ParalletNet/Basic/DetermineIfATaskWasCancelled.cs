using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParalletNet.Basic
{
    internal class DetermineIfATaskWasCancelled
    {
        public static void Run()
        {
            CancellationTokenSource tokenSource1 = new CancellationTokenSource();
            CancellationToken token = tokenSource1.Token;

            Task task1 = new Task(() => {
                for (int i = 0; i < int.MaxValue; i++)
                {
                    token.ThrowIfCancellationRequested();
                    Console.WriteLine("Task 1 - Int value {0}", i);
                }
            }, token);

            CancellationTokenSource tokenSource2 = new CancellationTokenSource();
            CancellationToken token2 = tokenSource2.Token;

            Task task2 = new Task(() => { 
                
            });

            tokenSource2.Cancel();

            Console.WriteLine(task1.IsCanceled);
            Console.WriteLine(task2.IsCanceled);

        }
    }
}
