//using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace openWeather.Utils.Settings
{
    public class AppSettings
    {
        public Uri BaseUrl { get; set; }
        public string Apikey { get; set; }

        public AppSettings()
        {
            BaseUrl = new Uri("https://api.openweathermap.org/data/2.5");
            Apikey = "fc3e204c531c22ed317028b9c045e7e5";
        }

    }
}
