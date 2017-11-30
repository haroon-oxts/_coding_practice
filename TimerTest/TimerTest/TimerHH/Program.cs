using System;
using System.Threading;

namespace TimerHH
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting a bitchin ass timer on thread {0}", Thread.CurrentThread.ManagedThreadId);

            //Timer t = new Timer(PrintStuff,null,TimeSpan.Zero,TimeSpan.FromMilliseconds(500));

            Timer t = new Timer((o) => 
            {
                Console.WriteLine("Shit is going down in thread {0}", Thread.CurrentThread.ManagedThreadId);
            }, 
            null, 
            TimeSpan.Zero, 
            TimeSpan.FromMilliseconds(500));



            Console.ReadKey();

        }

        static void PrintStuff(object pay_load)
        {
            Console.WriteLine("Shit is going down in thread {0}",Thread.CurrentThread.ManagedThreadId);
        }
    }
}
