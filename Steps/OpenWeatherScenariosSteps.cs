using FluentAssertions;
using openWeather.Apis;
using openWeather.Apis.Suporte;
using openWeather.Base;
using openWeather.Models;
using openWeather.Utils.Hooks;
using System.Globalization;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Infrastructure;

namespace openWeather.Steps
{
    [Binding]
    public class OpenWeatherScenariosSteps
    {
        #region Instâncias
        string latitude = "", longitude = "";
        public BaseRequest<CurrentWeatherData> _request = new();
        private IExecuteOpenWeatherApi? _execute;
        private readonly ISpecFlowOutputHelper _outputHelper;

        public OpenWeatherScenariosSteps(ISpecFlowOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        #endregion

        #region Hooks

        [BeforeScenario("OpenWeather", Order = 0)]
        public void IniciarServiceLocator()
        {
            _execute = ServiceLocator.Current.GetInstance<IExecuteOpenWeatherApi>();
        }

        [AfterScenario("OpenWeather", Order = 0)]
        public void GerarRelatorioLivingDoc()
        {
            if (_request != null)
            {
                LivingDoc<CurrentWeatherData>.GerarRelatorioTestes(_outputHelper, _request);
            }
        }

        #endregion

        #region Given 
        [Given(@"a latitude (.*) de um determinado local")]
        public void GivenALatitudeDeUmDeterminadoLocal(string latitude)
        {
            this.latitude = latitude;
        }

        [Given(@"a longitude (.*) do mesmo local")]
        public void GivenALongitudeDoMesmoLocal(string longitude)
        {
            this.longitude = longitude;
        }

        [Given(@"que apenas a latitude (.*) é informada e a longitude não é")]
        public void GivenQueApenasALatitudeEInformadaEALongitudeNaoE(string latitude)
        {
            this.latitude = latitude;
        }

        #endregion

        #region When
        [When(@"a api de obtenção de clima é acionada")]
        public async Task WhenAApiDeObtencaoDeClimaEAcionada()
        {
            if (_request != null && _execute != null)
                _request = await _execute.RetornarDadoMeteorologicoAtual(latitude, longitude);
        }

        #endregion

        #region Then
        [Then(@"a API deverá retornar status (.*)")]
        public void ThenAAPIDeveraRetornarStatus(int statusCodeEsperado)
        {
            ((int)_request.StatusCode).Should().Be(statusCodeEsperado);
        }

        [Then(@"é retornado os dados meteorológicos atuais do local pré determinado (.*)")]
        public void ThenERetornadoOsDadosMeteorologicosAtuaisDoLocalPreDeterminado(string local)
        {
            _request.Response.Coord.Lat.Should().BeApproximately(double.Parse(latitude, CultureInfo.InvariantCulture), 2);
            _request.Response.Coord.Lon.Should().BeApproximately(double.Parse(longitude, CultureInfo.InvariantCulture), 2);
            _request.Response.Name.Should().Be(local);
        }

        #endregion

    }
}
