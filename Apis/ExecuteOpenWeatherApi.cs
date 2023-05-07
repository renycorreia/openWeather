using openWeather.Base;
using openWeather.Models;
using openWeather.Utils;
using openWeather.Utils.Settings;

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

        public async Task<BaseRequest<CurrentWeatherData>> RetornarDadoMeteorologicoAtual(string latitude, string longitude, string? unidade)
        {
            var url = $"{_appSettings.BaseUrl}/weather?lat={latitude}&lon={longitude}";

            if (unidade != null)
                url += $"{CommonFunctions.ConjutorDeParametroDaUrl(url)}units={unidade}";

            var request = await _restManager.Get<CurrentWeatherData>(url);

            return request;
        }
    }
}
