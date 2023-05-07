using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using openWeather.Base;
using System.Net;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Infrastructure;

namespace openWeather.Utils.Hooks
{
    [Binding]
    public static class LivingDoc<T>
    {
        public static void GerarRelatorioTestes(ISpecFlowOutputHelper outputHelper, BaseRequest<T> responseBody)
        {
            JsonSerializerSettings config = new JsonSerializerSettings();

            outputHelper.WriteLine("Informações adicionais: \n");
            outputHelper.WriteLine($"Método: {responseBody.Request.Method} \n");
            outputHelper.WriteLine($"URL: {responseBody.Request.Resource} \n");
            outputHelper.WriteLine($"Parâmetros");

            foreach (var headers in responseBody.Request.Parameters)
            {
                config.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;

                var serialize = JsonConvert.SerializeObject(headers.Value, Formatting.Indented, config);

                outputHelper.WriteLine($"{headers.Type} \n {headers.Name}: {serialize}\n");
            }

            outputHelper.WriteLine($"Default Headers \n");
            foreach (var defaultHeaders in responseBody.RestClient.DefaultParameters)
            {
                var serialize = JsonConvert.SerializeObject(defaultHeaders.Value, Formatting.Indented);
                outputHelper.WriteLine($"{defaultHeaders.Name} : {serialize}\n");
            }

            outputHelper.WriteLine($"Status Code: {(int)responseBody.StatusCode} \n");

            config.NullValueHandling = Newtonsoft.Json.NullValueHandling.Include;
            config.Converters.Add(new StringEnumConverter());

            switch (responseBody.StatusCode)
            {
                case 0:
                    var requestException = JsonConvert.SerializeObject(responseBody.ErrorRequest, Formatting.Indented);
                    outputHelper.WriteLine($"Error Request:\n {requestException}");
                    break;
                case HttpStatusCode.InternalServerError:
                case HttpStatusCode.BadRequest:
                    var responseBodyException = JsonConvert.SerializeObject(responseBody.Exception, Formatting.Indented);
                    outputHelper.WriteLine($"Response:\n {responseBodyException}");
                    break;
                default:
                    var response = JsonConvert.SerializeObject(responseBody.Response, Formatting.Indented, config);
                    outputHelper.WriteLine($"Response:\n {response}");
                    break;
            }
        }
    }
}