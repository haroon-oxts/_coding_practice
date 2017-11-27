using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace TaskWaitTest
{
    class Program
    {
        public static void Main()
        {
            CancellationTokenSource cancellation_token_source = new CancellationTokenSource();

            Stopwatch sw = new Stopwatch();


            Task<int> t = Task.Run(() => { return StartWork(cancellation_token_source.Token); });
            Task t2 =  t.ContinueWith((i) => { AfterWork(i.Result); });
            sw.Start();



            if (!t.Wait(TimeSpan.FromMilliseconds(2000)))
            {
                sw.Stop();
                Console.WriteLine("Task timed out, cancelling task. Stopwatch shows " + sw.ElapsedMilliseconds + " milliseconds");
                cancellation_token_source.Cancel();
                Thread.Sleep(1000);
                Console.WriteLine("Antecedent task status " + t.Status);
                Console.WriteLine("Continuation task status " + t2.Status);
            }

            Console.ReadKey();
        }

        public static Task<int> StartWork(CancellationToken cancellation_token)
        {
            Random rnd = new Random();
            for (int ctr = 1; ctr <= 30; ctr++)
            {
                if (cancellation_token.IsCancellationRequested)
                {
                    return Task.FromResult(ctr);
                }
                else
                {
                    Console.WriteLine("Heres a random number {0}", rnd.Next(0,10));
                    Thread.Sleep(100);
                }
            }

            return Task.FromResult(50000);
        }

        public static void AfterWork(int input)
        {
            Console.WriteLine("Runnig AfterWork, StartWork loop ran " + input + " times");

        }
    }
}
