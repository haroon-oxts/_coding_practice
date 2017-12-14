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
        static CancellationTokenSource CTS;

        static void Main(string[] args)
        {

            List<Task> task_list = RunTasks();

            foreach (Task t in task_list)
            {
                Console.WriteLine("Task " + t.Id + " :" + "Status - " + t.Status);
            }

            Task waiting_task = WaitForAll(task_list);

            Console.WriteLine("-------Waiting for all tasks to complete------");


            Console.WriteLine("Press a key to cancel tasks");

            while (!waiting_task.IsCompleted)
            {
                Console.ReadKey();
                CTS.Cancel();
            }

            foreach (Task t in task_list)
            {
                Console.WriteLine("Task " + t.Id + " :" + "Status - " + t.Status);
            }

            Console.WriteLine("Main Task :" + "Status - " + waiting_task.Status);

            Console.ReadKey();
        }


        private static Task WaitForAll(List<Task> task_list)
        {
            return Task.WhenAll(task_list);
        }

        private static List<Task> RunTasks()
        {
            CTS = new CancellationTokenSource();

            List<Task> task_list = new List<Task>(); 

            task_list.Add(Test(CTS.Token));
            Console.WriteLine("Created task " + task_list[0].Id + " with cts: " + CTS.GetHashCode());

            task_list.Add(Test(CTS.Token));
            Console.WriteLine("Created task " + task_list[1].Id + " with cts: " + CTS.GetHashCode());

            task_list.Add(Test(CTS.Token));
            Console.WriteLine("Created task " + task_list[2].Id + " with cts: " + CTS.GetHashCode());

            return task_list;
        }

        private static Task Test(CancellationToken cancellation_token)
        {
            Task t = null;
            Random r = new Random();
            int steps_to_complete = r.Next(3,8);
        
            t = Task.Run(() => {

                while (true)
                {
                    try
                    {
                        cancellation_token.ThrowIfCancellationRequested();
                    }
                    catch
                    {
                        Console.WriteLine("Task " + t.Id + " : " + " cancellation requested with token " + cancellation_token.GetHashCode() + ", IsCancellationRequested status: " + cancellation_token.IsCancellationRequested);
                        return;
                    }

                    Console.WriteLine("Task " + t.Id + " : " + "on thread " + Thread.CurrentThread.ManagedThreadId + 
                                      ", with token " + cancellation_token.GetHashCode() + 
                                      ", IsCancellationRequested: " + cancellation_token.IsCancellationRequested + 
                                      ", status - " + t.Status + 
                                      ", steps to complete " + steps_to_complete);
                    
                    Task.Delay(1000).Wait();
                    steps_to_complete--;

                    if(steps_to_complete <= 0)
                    {
                        return;
                    }
                }

            }, cancellation_token);

            return t;
        }

    }

}
