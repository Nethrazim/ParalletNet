using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;

namespace ParalletNet.Basic
{
    class CancellingTasks
    {
        public static void Run()
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();

            CancellationToken token = tokenSource.Token;

            Task task = new Task(() =>
            {
                for (int i = 0; i < int.MaxValue; i++)
                {
                    if (token.IsCancellationRequested)
                    {
                        Console.WriteLine("Task cancel detected");
                        throw new OperationCanceledException(token);
                    }
                    else
                    {
                        Console.WriteLine("Int value {0}", i);
                    }
                }
            }, token);

            task.Start();
            tokenSource.Cancel();


            CancellationTokenSource tokenSource2 = new CancellationTokenSource();
            CancellationToken token2 = tokenSource2.Token;

            Task task2 = new Task(() => {
                for (int i = 0; i < int.MaxValue; i++)
                {
                    if (token2.IsCancellationRequested)
                        throw new OperationCanceledException(token2);
                    else
                    {
                        Console.WriteLine("Int value {0}", i);
                    }
                }
            }, token2);
            token2.Register(() => {
                Console.WriteLine(">>>> Delegate Invoked\n");
            });

            task2.Start();
            tokenSource2.Cancel();
        }
    }
}
