using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParalletNet.Basic
{
    internal class ExcessiveSpinning
    {
        public static void Run()
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();

            Task t1 = Task.Factory.StartNew(() => {
                Console.WriteLine("Task 1 waiting for cancellation");
                tokenSource.Token.WaitHandle.WaitOne();
                Console.WriteLine("Task 1 cancelled");
                tokenSource.Token.ThrowIfCancellationRequested();
            }, tokenSource.Token);

            Task t2 = Task.Factory.StartNew(() => {
                // enter a loop until t1 is cancelled
                while (!t1.Status.HasFlag(TaskStatus.Canceled))
                {
                    // do nothing - this is a code loop
                }
                Console.WriteLine("Task 2 exited code loop");
            });

            Task t3 = Task.Factory.StartNew(() => {
                // enter the spin wait loop
                while (t1.Status != TaskStatus.Canceled)
                {
                    Thread.SpinWait(1000);
                }
                Console.WriteLine("Task 3 exited spin wait loop");
            });


            tokenSource.Cancel();
        }
    }
}
