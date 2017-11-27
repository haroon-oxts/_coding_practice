using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;

namespace Speech
{
    class Program
    {

        static Prompt current_prompt;
        static SpeechSynthesizer synth = new SpeechSynthesizer();

        static  string long_ass_sentence = "I'm gonna read this long ass sentence that will probably take some time so if you want to interrup me then just press a key other wise I wil probably just keep going. Did you know the worlds smallest animal is the rodent juggly poo arse shiticus ? Well you just learned something new today. I hate you.";
        static  string greeting = "Hi there buttmunch";
        static  string normal_sentence = "I'm thirsty";
        
        static void Main(string[] args)
        {

            synth.Speak(greeting);
            synth.Speak(normal_sentence);
            synth.Speak(normal_sentence);
            synth.Speak(normal_sentence);

            Console.WriteLine("doing other stuff");

            //Task.Run(() =>
            //{
            //    while (true)
            //    {
            //        if (current_prompt.IsCompleted)
            //        {
            //            current_prompt = synth.SpeakAsync(normal_sentence);
            //        }
            //    }
            //});



            Console.ReadKey();

        }


    }
}
