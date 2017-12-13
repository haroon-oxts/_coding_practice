using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskTest
{
    class Program
    {

        static void Main(string[] args)
        {
            TaskRunningObject t = new TaskRunningObject();

            //Console.WriteLine("Running task");
            //t.StartTask();

            //Console.WriteLine("***Main thread***");

            //Console.WriteLine("***press key to cancel the task***");
            //Console.ReadKey();
            //t.CTS.Cancel();
            //Console.WriteLine("***immediate report on t object***");
            //Console.WriteLine("t object CTS status: " + t.CTS.IsCancellationRequested);
            //Console.WriteLine("t object Task status: " + t.CurrentTask.Status);


            //Console.WriteLine("***press key to create a new CTS and run task***");
            //Console.ReadKey();
            //t.StartTask();
            //Console.WriteLine("***immediate report on t object***");
            //Console.WriteLine("t object CTS status: " + t.CTS.IsCancellationRequested);
            //Console.WriteLine("t object Task status: " + t.CurrentTask.Status);

            Test();

            Console.ReadKey();
        }

        private async static void Test()
        {

            Task t = Task.Run(() => { Thread.Sleep(2000); return 5; });

            t.Start();

            int x = t.tres


            Console.WriteLine(x);

        }

    }




    class TaskRunningObject
    {
        public CancellationTokenSource CTS { get; set; }
        public Task CurrentTask            { get; set; }

        public Task StartTask()
        {
            CTS = new CancellationTokenSource();

            CurrentTask = ThingToDo(TimeSpan.FromMilliseconds(2000), CTS.Token);

            return CurrentTask;
        }

        private async Task ThingToDo(TimeSpan polling_timespan, CancellationToken cancellation_token)
        {
            Task current_task = null;

            //current_task = Task.Run(() => {

            //    while (true)
            //    {
            //        cancellation_token.ThrowIfCancellationRequested();

            //        //Console.WriteLine("------------Task Update-----------");
            //        //Console.WriteLine("CurrentTask status: " + current_task.Status);
            //        //Console.WriteLine("CurrentTask polling period: {0} Milliseconds", polling_timespan.TotalMilliseconds);
            //        //Console.WriteLine("CurrentTask token cancellation status: " + cancellation_token.IsCancellationRequested);

            //        //Task.Delay(polling_timespan, cancellation_token).Wait();
            //    }

            //},cancellation_token);

            //while(current_task.Status != TaskStatus.RanToCompletion)
            //{
            //    Console.WriteLine("CurrentTask status: " + current_task.Status);
            //}

            current_task = Task.Run(() =>
            {
                //for (int i = 10; i < 432543543; i++)
                //{
                //    // just for a long job
                //    double d3 = Math.Sqrt((Math.Pow(i, 5) - Math.Pow(i, 2)) / Math.Sin(i * 8));
                //}

                while (true)
                {


                }

                //return "Foo Completed.";

            },cancellation_token);

            while (current_task.Status != TaskStatus.RanToCompletion)
            {
                Console.WriteLine("Thread ID: {0}, Status: {1}", Thread.CurrentThread.ManagedThreadId, current_task.Status);

            }

            await current_task;
        }


        //private static async Task<string> Foo(int seconds)
        //{
        //    await Task.Run(() =>
        //    {
        //        for (int i = 0; i < seconds; i++)
        //        {
        //            Console.WriteLine("Thread ID: {0}, second {1}.", Thread.CurrentThread.ManagedThreadId, i);
        //            Task.Delay(TimeSpan.FromSeconds(1)).Wait();
        //        }

        //        // in here don't return anything
        //    });

        //    return await Task.Run(() =>
        //    {
        //        for (int i = 0; i < seconds; i++)
        //        {
        //            Console.WriteLine("Thread ID: {0}, second {1}.", Thread.CurrentThread.ManagedThreadId, i);
        //            Task.Delay(TimeSpan.FromSeconds(1)).Wait();
        //        }

        //        return "Foo Completed.";
        //    });
        //}

    }
}
