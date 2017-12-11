using OxTS.NavLib.Common.Measurement;
using OxTS.NavLib.DataStoreManager.Manager;
using OxTS.NavLib.StreamItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealTimeDataStoreManagerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            RealTimeDataStoreManager rtdsman = new RealTimeDataStoreManager();

            IStreamItem chosen_stream;

            Thread.Sleep(5000);

            chosen_stream = ChooseStream(rtdsman.GetAllStreamItems());

            List<Tuple<uint, string>> stream_measurements = new List<Tuple<uint, string>>
                                                                {
                                                                    new Tuple<uint, string>(chosen_stream.StreamId, "Ax"),
                                                                    new Tuple<uint, string>(chosen_stream.StreamId, "Ay"),
                                                                    new Tuple<uint, string>(chosen_stream.StreamId, "Az"),
                                                                    new Tuple<uint, string>(chosen_stream.StreamId, "InsNavMode"),
                                                                };

            rtdsman.ConfigureMeasurementsToRead(stream_measurements, 1, OxTS.NavLib.Common.Enums.MeasurementType.Multiple);

            while (true)
            {

                List<MeasurementValue> output_measurements = rtdsman.GetRealTimeData(1).GetData();

                Console.WriteLine("---------------------------------------------------------------------------");

                Console.WriteLine("{0,-10}{1,-17}{2,-8}{3,-15}{4,-6}{5,-15}{6,-15}", "StreamID", 
                                                                                     "OriginalStreamId", 
                                                                                     "Alive", 
                                                                                     "Type", 
                                                                                     "SN", 
                                                                                     "Model", 
                                                                                     "Lag Time");
                Console.WriteLine("{0,-10}{1,-17}{2,-8}{3,-15}{4,-6}{5,-15}{6,-15}", chosen_stream.StreamId, 
                                                                                     chosen_stream.OriginalStreamId, 
                                                                                     chosen_stream.Alive, 
                                                                                     chosen_stream.CodecType, 
                                                                                     chosen_stream.DeviceSerialNumber, 
                                                                                     chosen_stream.ProductModel, 
                                                                                     chosen_stream.LagTime);

                Console.WriteLine("\n");

                //Output data using our dictionary to find which value corresponds to which name
                if (output_measurements.Count > 0)
                {
                    for (int i = 0; i < output_measurements.Count; ++i)
                    {
                        Console.WriteLine(output_measurements[i].MeasurementName + ":" + output_measurements[i].ValueString);
                    }
                }

                Thread.Sleep(1000);
            }
        }

        public static IStreamItem ChooseStream(List<IStreamItem> streams)
        {
            Console.WriteLine("{0,-10}{1,-17}{2,-6}{3,-15}", "StreamID", "Type", "SN", "Model");
            //Print basic information for the user to select the stream they want
            foreach (IStreamItem stream in streams)
            {
                Console.WriteLine("{0,-10}{1,-17}{2,-6}{3,-15}", stream.StreamId, stream.CodecType, stream.DeviceSerialNumber, stream.ProductModel);
            }
            Console.WriteLine("\n");

            //User selects a stream
            Console.WriteLine("Choose a stream to view:");

            uint chosen_stream_id = 0;
            while (true)
            {
                try
                {
                    chosen_stream_id = Convert.ToUInt32(Console.ReadLine());
                }
                catch (System.Exception e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }

                IStreamItem chosen_stream = streams.Find(x => x.StreamId == chosen_stream_id);

                if (chosen_stream != null)
                {
                    Console.WriteLine("The selected m_streams device serial number was " + chosen_stream.DeviceSerialNumber);
                    return chosen_stream;
                }
                else
                {
                    Console.WriteLine("Stream not found");
                }
            }
        }

    }
}
