using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace botApp
{
    class teleBot
    {
        int update_id = 0;
        string messageFromId = "";
        string messageText = "";
        static string token = "1360530315:AAH_-A-B7NlGmdoycdGVlWIrHExf8pTuy7w";
        string startUrl = $"https://api.telegram.org/bot{token}";

        public void Start()
        {
            WebClient client = new WebClient();
           

            while (true)
            {
                string url = $"{startUrl}/getUpdates?offset={update_id + 1}";
                string response = client.DownloadString(url);

                var array = JObject.Parse(response)["result"].ToArray();

                foreach (var msg in array)
                {
                    update_id = Convert.ToInt32(msg["update_id"]);
                    try
                    {
                        messageFromId = msg["message"]["from"]["id"].ToString();
                        messageText = msg["message"]["text"].ToString();
                        Console.WriteLine($"{update_id} {messageFromId} {messageText}");

                        if (messageText == "/kiev" || messageText== "/irpen" || messageText== "/boroda")
                        {
                            messageText = new WeatherClassStf().ParseXml(messageText);
                        }
                        else
                        {
                            messageText = "/kiev - Kiev\n/irpen - Irpen\n/boroda - Borodyanka";
                        }

                        url = $"{startUrl}/sendMessage?chat_id={messageFromId}&text={messageText}";
                        client.DownloadString(url);
                    }
                    catch
                    {
                    }
                }
                Thread.Sleep(50);
            }
        }
    }
}
