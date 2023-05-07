using openWeather.Base;
using openWeather.Models;

namespace openWeather.Apis
{
    public interface IExecuteOpenWeatherApi
    {
        Task<BaseRequest<CurrentWeatherData>> RetornarDadoMeteorologicoAtual(string latitude, string longitude, string? unidade);
    }
}
