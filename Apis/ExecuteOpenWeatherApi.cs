using openWeather.Base;
using openWeather.Models;
using openWeather.Utils.Settings;
using RestSharp;
using System.Threading.Tasks;
//using static openWeather.Utils.Settings.Settings;

namespace openWeather.Apis
{
    public class ExecuteOpenWeatherApi : IExecuteOpenWeatherApi
    {
        private readonly RestManager _restManager;
        private readonly AppSettings _appSettings;

        public ExecuteOpenWeatherApi(RestManager restManager,
                              AppSettings appSettings)
        {
            _restManager = restManager;
            _appSettings = appSettings;
        }

        public async Task<BaseRequest<CurrentWeatherData>> RetornarDadoMeteorologicoAtual(string latitude, string longitude)
        {
            var url = $"{_appSettings.BaseUrl}/weather?lat={latitude}&lon={longitude}";
            var request = await _restManager.Get<CurrentWeatherData>(url);

            return request;
        }
    }
}
