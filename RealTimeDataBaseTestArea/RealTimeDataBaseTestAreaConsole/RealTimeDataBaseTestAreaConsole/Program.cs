using OxTS.NavLib.Common.Measurement;
using OxTS.NavLib.DataStoreManager.Manager;
using OxTS.NavLib.MeasurementCategorisation;
using OxTS.NavLib.RealTime;
using OxTS.NavLib.StreamItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealTimeDataBaseTestAreaConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            RealTimeDataStoreManager rtdsman = new RealTimeDataStoreManager();

            Thread.Sleep(3000);

            List<IStreamItem> streams = rtdsman.GetAllStreamItems();

            foreach(IStreamItem stream in streams)
            {

                Console.WriteLine("---------");
                Console.WriteLine(stream.StreamId);
                Console.WriteLine(stream.StreamSerial);
                Console.WriteLine(stream.Address);

            }

            uint chosen_stream = Convert.ToUInt32(Console.ReadLine());

            ulong caller_id = 0;

            List<Tuple<uint, string>> stream_measurements = new List<Tuple<uint, string>>
            {
                new Tuple<uint, string>(chosen_stream, "Nano"),
                new Tuple<uint, string>(chosen_stream, "Ax"),
                //new Tuple<uint, string>(chosen_stream, "Ay"),
                //new Tuple<uint, string>(chosen_stream, "Az"),
                //new Tuple<uint, string>(chosen_stream, "Vn"),
                //new Tuple<uint, string>(chosen_stream, "Ve"),
                //new Tuple<uint, string>(chosen_stream, "Vd"),
                //new Tuple<uint, string>(chosen_stream, "VnAcc"),
                //new Tuple<uint, string>(chosen_stream, "VeAcc"),
                //new Tuple<uint, string>(chosen_stream, "VdAcc"),
            };

            Console.WriteLine("Configured unit {0}", chosen_stream);

            rtdsman.ConfigureMeasurementsToRead(stream_measurements,
                                                            caller_id, 
                                                            OxTS.NavLib.Common.Enums.MeasurementType.MultipleStreamed);

            rtdsman.GetRealTimeData(caller_id).SetHistorySize(TimeSpan.FromMilliseconds(30));

            Thread.Sleep(1000);


            while (true)
            {
                List<List<MeasurementValue>> output_measurements = rtdsman.GetRealTimeData(caller_id).GetStreamData();

                foreach( List<MeasurementValue> measurements_list in output_measurements)
                {
                    if(measurements_list.Count > 0)
                    {
                        for (int i = 0; i < measurements_list.Count; ++i)
                        {
                            Console.WriteLine(measurements_list[i].MeasurementName + " : " + measurements_list[i].ValueString);
                        }
                    }
                }

                Thread.Sleep(1500);
            }

        }
    }
}
