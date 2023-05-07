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
        string latitude = "", longitude = "", unidade;
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

        [Given(@"a unidade de medida ""([^""]*)"" em que deseja que os dados sejam retornados")]
        public void GivenAUnidadeDeMedidaEmQueDesejaQueOsDadosSejamRetornados(string unidade)
        {
            if (unidade != "standard")
                this.unidade = unidade;
        }

        #endregion

        #region When
        [When(@"a api de obtenção de clima é acionada")]
        public async Task WhenAApiDeObtencaoDeClimaEAcionada()
        {
            if (_request != null && _execute != null)
                _request = await _execute.RetornarDadoMeteorologicoAtual(latitude, longitude, unidade);
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
            var weather = _request.Response;

            weather.Coord.Lat.Should().Be(double.Parse(latitude, CultureInfo.InvariantCulture));
            weather.Coord.Lon.Should().Be(double.Parse(longitude, CultureInfo.InvariantCulture));
            weather.Name.Should().Be(local);
        }

        [Then(@"é retornado os dados meteorológicos na unidade escolhida ""([^""]*)""")]
        public void ThenERetornadoOsDadosMeteorologicosNaUnidadeEscolhida(string unidade)
        {
            var weather = _request.Response;
            if (unidade == "metric")
            {
                weather.Main.Temp.Should().BeInRange(3.3, 37.8); //menores e maiores temperaturas registradas para São Paulo até hoje
            }
            else if (unidade == "imperial")
            {
                weather.Main.Temp.Should().BeInRange(37.94, 100.04);
            }
            else
            {
                weather.Main.Temp.Should().BeInRange(276.45, 310.95);
            }
        }

        #endregion
    }
}
