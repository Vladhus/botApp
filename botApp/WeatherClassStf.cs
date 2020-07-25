using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace botApp
{
    public class WeatherClassStf
    {
        public string LoadRepository(string city)
        {
            Dictionary<string, string> tempRepository = new Dictionary<string, string>();
            tempRepository.Add("/kiev", @"https://www.eurometeo.ru/ukraina/kiyvska-oblast/kiyiv/export/xml/data/");
            tempRepository.Add("/irpen", @"https://www.eurometeo.ru/ukraina/kiyvska-oblast/irpin/export/xml/data/");
            tempRepository.Add("/boroda", @"https://www.eurometeo.ru/ukraina/kiyvska-oblast/borodyanka/export/xml/data");

            return tempRepository[city];
        }

        public string ParseXml(string city)
        {
            EnableIgnoreCertError();
            string loadXml = new WebClient().DownloadString(LoadRepository(city)); //url
           

            var temoColection = XDocument.Parse(loadXml).Descendants("weather").Descendants("city").Descendants("step").ToArray();

            string result = String.Empty;

            for (int i = 0; i < 4; i++)
            {
                result += $"{temoColection[i].Element("datetime").Value.Replace("05:00:00","night").Replace("11:00:00", "morning").Replace("17:00:00", "evening").Replace("23:00:00","techno")} " +
                   $" {temoColection[i].Element("temperature").Value}°c " +
                   $" {temoColection[i].Element("pressure").Value} mm.rt.st. \n";
            }
            DisableIgnoreCertError();
            return result;
        }

        public void EnableIgnoreCertError()
        {
            ServicePointManager.ServerCertificateValidationCallback =
                delegate { return true; };
        }

        public void DisableIgnoreCertError()
        {
            ServicePointManager.ServerCertificateValidationCallback =
                null;
        }
    }
}
