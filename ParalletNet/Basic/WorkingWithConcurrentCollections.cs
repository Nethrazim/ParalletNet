using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParalletNet.Basic
{
    public class WorkingWithConcurrentCollections
    {
        public static void Run()
        {
            Queue<int> sharedQueue = new Queue<int>();

            for (int i = 0; i < 1000; i++)
            {
                sharedQueue.Enqueue(i);
            }

            int itemCount = 0;

            Task[] tasks = new Task[10];
            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = new Task(() => {
                    while (sharedQueue.Count > 0)
                    {
                        int item = sharedQueue.Dequeue();
                        Interlocked.Increment(ref itemCount);
                    }
                });

                tasks[i].Start();
            }

            Task.WaitAll(tasks);

            Console.WriteLine("Items processed: {0}", itemCount);
        }


        public static void Run2()
        {
            ConcurrentQueue<int> sharedQueue = new ConcurrentQueue<int>();
            for (int i = 0; i < 1000; i++)
            {
                sharedQueue.Enqueue(i);
            }

            int itemCount = 0;

            Task[] tasks = new Task[10];
            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = new Task(() =>
                {
                    while (sharedQueue.Count > 0)
                    {
                        int queueElement;
                        bool gotElement = sharedQueue.TryDequeue(out queueElement);
                        if (gotElement)
                        {
                            Interlocked.Increment(ref itemCount);
                        }
                    }
                });

                tasks[i].Start();
            }

            Task.WaitAll(tasks);

            Console.WriteLine("Items processed: {0}", itemCount);
        }

        public static void Run3()
        {
            ConcurrentBag<int> sharedBag = new ConcurrentBag<int>();
            for (int i = 0; i < 1000; i++)
            {
                sharedBag.Add(i);
            }

            int itemCount = 0;
            Task[] tasks = new Task[10];
            for (int i = 0; i < tasks.Length; i++)
            {
                // create the new task
                tasks[i] = new Task(() =>
                {
                    while (sharedBag.Count > 0)
                    {
                        int queueElement;
                        bool gotElement = sharedBag.TryTake(out queueElement);
                        if (gotElement)
                        {
                            Interlocked.Increment(ref itemCount);
                        }
                    }
                });
                tasks[i].Start();
            }
            Task.WaitAll(tasks);
            Console.WriteLine("Items processed: {0}", itemCount);
        }
    }
}
