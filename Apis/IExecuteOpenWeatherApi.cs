using openWeather.Base;
using openWeather.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace openWeather.Apis
{
    public interface IExecuteOpenWeatherApi
    {
        Task<BaseRequest<CurrentWeatherData>> RetornarDadoMeteorologicoAtual(string latitude, string longitude);
    }
}
