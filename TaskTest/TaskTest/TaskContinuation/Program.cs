using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskContinuation
{
    class Program
    {
        static void Main(string[] args)
        {
            Task task1 = WriteString();

            Task task2 = task1.ContinueWith((t) => { WriteInteger();});

            Task task3 = task2.ContinueWith((t) => { WriteDouble(); });


            Console.ReadKey();
        }

        static Task WriteString()
        {
            return Task.Run(() => {
                Thread.Sleep(5000);
                Console.WriteLine("What the hell!!");
            });
        }

        static Task WriteInteger()
        {
            Random r = new Random();

            return Task.Run(() => {
                Thread.Sleep(5000);
                Console.WriteLine("Its a random int: " + r.Next());
            });
        }

        static Task WriteDouble()
        {
            Random r = new Random();

            return Task.Run(() => {
                Thread.Sleep(5000);
                Console.WriteLine("Its a random double: " + r.NextDouble());
            });
        }
    }
}
