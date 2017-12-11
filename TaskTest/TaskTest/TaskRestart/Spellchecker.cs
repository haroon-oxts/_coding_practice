using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskRestart
{
    class Spellchecker
    {
        Task pendingTask = null; // pending session
        CancellationTokenSource cts = null; // CTS for pending session

        // SpellcheckAsync is called by the client app
        public async Task<bool> SpellcheckAsync(CancellationToken token)
        {
            // SpellcheckAsync can be re-entered
            var previousCts = this.cts;
            var newCts = CancellationTokenSource.CreateLinkedTokenSource(token);
            this.cts = newCts;

            if (IsPendingSession())
            {
                // cancel the previous session and wait for its termination
                if (!previousCts.IsCancellationRequested)
                    previousCts.Cancel();

                // this is not expected to throw
                // as the task is wrapped with ContinueWith
                await this.pendingTask;
            }

            newCts.Token.ThrowIfCancellationRequested();
            var newTask = SpellcheckAsyncHelper(newCts.Token);

            this.pendingTask = newTask.ContinueWith((t) => {
                this.pendingTask = null;
                // we don't need to know the result here, just log the status
                Debug.Print(((object)t.Exception ?? (object)t.Status).ToString());
            }, TaskContinuationOptions.ExecuteSynchronously);

            return await newTask;
        }

        // the actual task logic
        async Task<bool> SpellcheckAsyncHelper(CancellationToken token)
        {
            // do not start a new session if the the previous one still pending
            if (IsPendingSession())
                throw new ApplicationException("Cancel the previous session first.");

            // do the work (pretty much IO-bound)
            try
            {
                bool doMore = true;
                while (doMore)
                {
                    token.ThrowIfCancellationRequested();

                    Console.WriteLine("Running SpellcheckAsyncHelper");
                    await Task.Delay(500); // placeholder to call the provider
                }
                return doMore;
            }
            finally
            {
                // clean-up the resources
            }
        }

        public bool IsPendingSession()
        {
            return this.pendingTask != null &&
                !this.pendingTask.IsCompleted &&
                !this.pendingTask.IsCanceled &&
                !this.pendingTask.IsFaulted;
        }
    }
}
