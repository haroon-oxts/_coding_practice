using OxTS.NavLib.Common.Measurement;
using OxTS.NavLib.DataStoreManager.Manager;
using OxTS.NavLib.StreamItems;
using System;
using System.Collections.Generic;
using System.IO;
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
                                                                    new Tuple<uint, string>(chosen_stream.StreamId, "Dist2d"),
                                                                    new Tuple<uint, string>(chosen_stream.StreamId, "Dist3d"),                 
                                                                    new Tuple<uint, string>(chosen_stream.StreamId, "Dist2dHold"),             
                                                                    new Tuple<uint, string>(chosen_stream.StreamId, "Dist3dHold"),
                                                                    new Tuple<uint, string>(chosen_stream.StreamId, "DigitalOut2Nano"),        
                                                                    new Tuple<uint, string>(chosen_stream.StreamId, "DigitalOut2UpdateCount"),
                                                                    new Tuple<uint, string>(chosen_stream.StreamId, "Nano"),
                                                                };

            rtdsman.ConfigureMeasurementsToRead(stream_measurements, 1, OxTS.NavLib.Common.Enums.MeasurementType.Multiple);                                                                                                                                                                        

            long last_known_nano = 0;

            using (StreamWriter file = new StreamWriter("log.txt", false))
            {
                for (int i = 0; i < stream_measurements.Count; ++i)
                {
                    file.Write(stream_measurements[i].Item2 + ", ");
                }

                file.WriteLine("");

                while (true)
                {
                    List<MeasurementValue> output_measurements = rtdsman.GetRealTimeData(1).GetData();

                    long current_nano = (long)output_measurements.Find(o => o.MeasurementName == "Nano").Value;

                    //Output data using our dictionary to find which value corresponds to which name
                    if (output_measurements.Count > 0)
                    {
                        if ( current_nano != last_known_nano)
                        {
                            for (int i = 0; i < output_measurements.Count; ++i)
                            {
                                file.Write(output_measurements[i].Value + ", ");
                            }
                            file.WriteLine("");
                        }

                    }
                    else
                    {
                        file.WriteLine("List is empty " + DateTime.Now.Millisecond);
                    }

                    last_known_nano = current_nano;
                }
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
