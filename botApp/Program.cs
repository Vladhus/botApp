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
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(new WeatherClassStf().ParseXml("/irpen"));
            teleBot bot = new teleBot();
            bot.Start();

           



            Console.ReadKey();
            
        }

        
    }
}
