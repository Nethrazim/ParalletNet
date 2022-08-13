using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ParalletNet.Basic
{
    internal class LocalVariableEvaluation
    {
        public static void Run()
        {
            for (int i = 0; i < 5; i++)
            {
                Task.Factory.StartNew(() =>
                {
                    Console.WriteLine("Task {0} has counter value: {1}", Task.CurrentId, i);
                });
            }

            for (int i = 0; i < 5; i++)
            {
                Task.Factory.StartNew((stateObj) =>
                {
                    int loopValue = (int)stateObj;
                    Console.WriteLine("Task {0} has counter value: {1}", Task.CurrentId, loopValue);
                }, i);
            }
        }
    }
}
