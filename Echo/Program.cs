using System;
using System.Linq;
using System.Net;
using System.Xml.Linq;

namespace Echo
{  
    static class Program
    {
        static void Main()
        {
            bool SystemsNominal = true;
            bool networkOnline = true;

            string temperature = "unavailable";
            string conditions = "unavailable";

            //attempt to get weather information from Wunderground api.
            try
            {
                //WUNDERGROUND API KEY (500 requests per day, 10 per minute) = cd90a090e8086362
                XDocument xml = XDocument.Load("http://api.wunderground.com/api/cd90a090e8086362/hourly/q/autoip.xml");

                //locate the most recent weather report
                var forecast = xml.Descendants("forecast").First();

                //get the temperature and conditions out of the report
                temperature = forecast.Descendants("temp").First().Descendants("metric").First().Value;
                conditions = forecast.Descendants("condition").First().Value;
            }
            catch (WebException) //error, we cant be fully online or the weather api is down/has changed!
            {
                networkOnline = false;
            }
            catch (Exception)
            {

            }

            //get system stats
            var cpuSpeed = (int)SystemMonitor.Instance.GetCPU0Speed();
            var cpuTemp = (int)SystemMonitor.Instance.GetCPU0Temp();
            var gpuSpeed = (int)SystemMonitor.Instance.GetGPU0Speed();
            var gpuTemp = (int)SystemMonitor.Instance.GetGPU0Temp();

            //warn user if systems are not nominal.
            if(cpuSpeed < 3000 
                || cpuSpeed > 5000
                || cpuTemp < 10
                || cpuTemp > 70
                || !networkOnline)
            {
                SystemsNominal = false;
            }

            //Set up speech generator
            System.Speech.Synthesis.SpeechSynthesizer voice = new System.Speech.Synthesis.SpeechSynthesizer();
            voice.SetOutputToDefaultAudioDevice();

            //speak basic information about computer
            voice.Speak("Greetings " + Environment.UserName);
            voice.Speak("Network connection, " + (networkOnline ? "Online" : "Offline"));
            voice.Speak("Core CPU Temperature, " + cpuTemp + " Degrees.");
            voice.Speak("Core CPU Speed, " + cpuSpeed  + " Megahertz.");
            voice.Speak("Core GPU Temperature, " + gpuTemp + " Degrees.");
            voice.Speak("Core GPU Speed, " + gpuSpeed + " Megahertz.");
            voice.Speak(temperature != string.Empty ? "Ambient temperature, " + temperature + " Degrees." : string.Empty);
            voice.Speak(conditions != string.Empty ? "The ambient weather conditions are, " + conditions : string.Empty);
            voice.Speak("The date is " + DateTime.Now.ToString("dddd, MMMM dd, yyyy"));
            voice.Speak("Local time is, " + DateTime.Now.ToString("HH,mm"));
            voice.Speak(SystemsNominal ? "All systems nominal" : "Error in startup procedure, systems are not nominal");       
        }
    }
}
