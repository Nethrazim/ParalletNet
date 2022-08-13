using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ParalletNet.Basic
{
    public class TaskBasic
    {
        public static async void Run()
        {
            await Task.Factory.StartNew(() => {
                Console.WriteLine("Hello World");
            });

            Predicate<string> amethod = new Predicate<string>(hasCharacter);
            Func<string, char, string> amethod2 = new Func<string, char, string>(transform);
            Action<string, char, string> amethod3 = new Action<string, char, string>(transform);

            Task task1 = new Task(new Action<object>(printMessage), "First Message");

            Task task2 = new Task(delegate (object obj)
            {
                printMessage(obj);
            }, "Second Task");

            Action<object> method2 = delegate (object obj) { };

            Task task3 = new Task((obj) => printMessage(obj), "Third task");

            Task task4 = new Task((obj) => {
                printMessage(obj);
            }, "Fourth Task");


            task1.Start();
            task2.Start();
            task3.Start();
            task4.Start();


            string[] messages = { "First task", "Second task", "Third task", "Fourth task" };
            foreach (string msg in messages)
            {
                Task myTask = new Task(obj => printMessage((string)obj), msg);
                myTask.Start();
            }
        }

        static void printMessage(object message)
        {
            Console.WriteLine(message.ToString());
        }

        static bool hasCharacter(string str)
        {
            return str.Contains('c');
        }

        static string transform(string str, char c)
        {
            return str + c;
        }

        static void transform(string str, char c, string str2)
        {
            str = str + c + str2;
        }
    }
}
