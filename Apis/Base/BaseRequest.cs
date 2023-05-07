using Newtonsoft.Json;
using RestSharp;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace openWeather.Base
{
    public class BaseRequest<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public T Response { get; set; }
        public RestRequest Request { get; set; }
        public RestClient RestClient { get; set; }
        public Erro Exception { get; set; }
        public string ErrorRequest { get; set; }
    }

    public class Erro
    {
        public string Codigo { get; set; }
        public string Mensagem { get; set; }
    }
}
