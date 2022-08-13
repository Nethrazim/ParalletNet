using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParalletNet.Basic
{
    internal class WaitingForTasks
    {
        public static void Run()
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            CancellationToken token = tokenSource.Token;

            Task task = createTask(token);
            task.Start();

            task.Wait();

            task = createTask(token);
            task.Start();

            bool completed = task.Wait(2000);
        }

        static Task createTask(CancellationToken token) { 
            return new Task(() => {
                for (int i = 0; i < 10; i++)
                {
                    token.ThrowIfCancellationRequested();
                    token.WaitHandle.WaitOne(1000);
                }
            }, token);
        }
    }
}
