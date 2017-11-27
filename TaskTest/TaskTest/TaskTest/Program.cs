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

            t.StartThresholdCheck();

            while (true)
            {
                Console.WriteLine("***Main thread***");

                Thread.Sleep(1000);
            }
        }

    }

    class TaskRunningObject
    {
        public TaskRunningObject()
        {
            m_threshold_check_cancellation_token_source = new CancellationTokenSource();
        }

        public CancellationTokenSource m_threshold_check_cancellation_token_source { get; set; }

        public void StartThresholdCheck()
        {
            Task task_to_await = StartThresholdCheckAsync(TimeSpan.FromMilliseconds(1001), m_threshold_check_cancellation_token_source.Token);
        }

        private async Task StartThresholdCheckAsync(TimeSpan polling_timespan, CancellationToken cancellation_token)
        {
            while (true)
            {
                ThresholdCheck();

                Console.WriteLine("Ran ThresholdCheck(), timespan is {0} Ticks", polling_timespan.Ticks);
                Console.WriteLine("Ran ThresholdCheck(), timespan is {0} Milliseconds", polling_timespan.TotalMilliseconds);
                Console.WriteLine("Ran ThresholdCheck(), timespan as a string : {0} ", polling_timespan.ToString());
                Console.WriteLine("Ran ThresholdCheck(), timespan is {0} Second component", polling_timespan.Seconds);
                Console.WriteLine("Ran ThresholdCheck(), timespan is {0} Milliseconds component", polling_timespan.Milliseconds);

                await Task.Delay(polling_timespan, cancellation_token);
            }

        }

        private void ThresholdCheck()
        {
            //if (IMUHeatMap.WarmedMaxAx > WarmUpPlugin.CurrentDevice.DeviceThresholds.WarmedMaxAx) { WarmUpPlugin.CurrentDevice.DeviceThresholds.AxPass = true; }
            //if (IMUHeatMap.WarmedMaxAy > WarmUpPlugin.CurrentDevice.DeviceThresholds.WarmedMaxAy) { WarmUpPlugin.CurrentDevice.DeviceThresholds.AyPass = true; }
            //if (IMUHeatMap.WarmedMaxAz > WarmUpPlugin.CurrentDevice.DeviceThresholds.WarmedMaxAz) { WarmUpPlugin.CurrentDevice.DeviceThresholds.AzPass = true; }

            //if (IMUHeatMap.WarmedMaxWx > WarmUpPlugin.CurrentDevice.DeviceThresholds.WarmedMaxWx) { WarmUpPlugin.CurrentDevice.DeviceThresholds.WxPass = true; }
            //if (IMUHeatMap.WarmedMaxWy > WarmUpPlugin.CurrentDevice.DeviceThresholds.WarmedMaxWy) { WarmUpPlugin.CurrentDevice.DeviceThresholds.WyPass = true; }
            //if (IMUHeatMap.WarmedMaxWz > WarmUpPlugin.CurrentDevice.DeviceThresholds.WarmedMaxWz) { WarmUpPlugin.CurrentDevice.DeviceThresholds.WzPass = true; }
        }
    }
}
