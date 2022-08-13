using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParalletNet.Basic
{
    internal class CancellingTaskWithHandle
    {
        public static void Run()
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            CancellationToken token = tokenSource.Token;
            try
            {
                Task task1 = new Task(() => {
                    for (int i = 0; i < int.MaxValue; i++)
                    {
                        if (token.IsCancellationRequested)
                        {
                            throw new OperationCanceledException(token);
                        }
                        else
                        {
                            Console.WriteLine("Int value {0}", i);
                        }

                    }
                }, token);


                Task task2 = new Task(() => {
                    token.WaitHandle.WaitOne();
                    Console.WriteLine(">>>>> wait handle released");
                });

                task1.Start();
                task2.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception ");
            }
            
            Thread.Sleep(500);
            tokenSource.Cancel();
        }
    }
}
