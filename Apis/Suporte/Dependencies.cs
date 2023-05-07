using Autofac;
using Dapper;
using openWeather.Base;
using openWeather.Utils.Settings;
using RestSharp;

namespace openWeather.Apis.Suporte
{
    public static class Dependencies
    {
        public static ContainerBuilder CreateContainerBuilder()
        {
            DapperBinds();
            var builder = new ContainerBuilder();
            builder.RegisterType<AppSettings>().SingleInstance();
            builder.RegisterType<RestClient>().As<RestClient>();
            builder.RegisterType<RestClientOptions>().As<RestClientOptions>();
            builder.RegisterType<RestManager>().As<RestManager>();
            builder.RegisterType<ExecuteOpenWeatherApi>().As<IExecuteOpenWeatherApi>();
            return builder;
        }

        private static void DapperBinds()
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }
    }
}