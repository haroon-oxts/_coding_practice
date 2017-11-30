using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace PeriodicTaskWithTimeout
{
    class Program
    {
        static CancellationTokenSource cancellation_token_source = new CancellationTokenSource();
        static CancellationToken cancellatian_token = cancellation_token_source.Token;

        static void Main(string[] args)
        {
            Console.WriteLine("Starting the periodic task");

            //Instantiate the progress to show the seconds elapsed
            Progress<int> test_progress = new Progress<int>((o) => { Console.WriteLine("Seconds elapsed: " + o/(double)1000); });


            PeriodicWithTimeout(TimeSpan.FromMilliseconds(500), 
                                TimeSpan.FromMilliseconds(10000),
                                test_progress).ContinueWith(ContinuationTask);

            Console.WriteLine("Other stuff is happening in main!!");



            Console.ReadKey();
        }

        static Task PeriodicWithTimeout( TimeSpan period, TimeSpan time_out, IProgress<int> progress)
        {
            Task periodic_task = Task.Run(() =>
            {
                Stopwatch stop_watch = new Stopwatch();
                stop_watch.Start();

               while (!cancellatian_token.IsCancellationRequested)
                {
                    Console.WriteLine("     Sleeping on thread: " + Thread.CurrentThread.ManagedThreadId);

                    //Report elapsed milliseconds
                    progress?.Report((int)stop_watch.ElapsedMilliseconds);

                    //Need to have a try catch here in order to catch the expection thrown Task.Delay 
                    try
                    {
                        Task.Delay(period, cancellatian_token).Wait();
                    }
                    catch
                    {
                        stop_watch.Reset();
                        return;
                    }
                }
                stop_watch.Reset();
                return;
            }, cancellatian_token);

            if (!periodic_task.Wait(time_out))
            {
                cancellation_token_source.Cancel();
            };

            //Return the task that either timed out or ran to completion
            return periodic_task;
        }

        static void ContinuationTask(Task input_task)
        {
            Console.WriteLine("Running the contiuation task, the passed task status was: " + input_task.Status);
        }

    }
}
