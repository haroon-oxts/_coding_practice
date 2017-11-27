using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;

namespace SpeechTestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            SpeechSynthesizer s = new SpeechSynthesizer();

            s.SpeakCompleted += S_SpeakCompleted;

            s.SpeakAsync("Thou speak'st aright; I am that merry wanderer of the night. I jest to Oberon and make him smile");

            Console.ReadKey();


        }

        private static void S_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
        {
            Console.WriteLine("Speech finished");

        }

    }
}
