using openWeather.Utils.Settings;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using System;
using System.Net;
using System.Threading.Tasks;

namespace openWeather.Base
{
    public class RestManager
    {
        protected RestClient RestClient;
        protected readonly RestClientOptions RestClientOptions;
        protected readonly AppSettings AppSettings;

        public RestManager(
            RestClientOptions restClientOptions,
                           AppSettings appSettings)
        {
            RestClientOptions = restClientOptions;
            AppSettings = appSettings;
        }

        public async Task<BaseRequest<TResponse>> Get<TResponse>(string uri)
        {
            var restRequest = Request(uri, Method.Get);
            return await GetContent<TResponse>(restRequest);
        }

        public async Task<BaseRequest<TResponse>> Post<TResponse, TBody>(string uri, TBody body)
        {
            var restRequest = BodyRequest(uri, body, Method.Post);
            return await GetContent<TResponse>(restRequest);
        }

        public async Task<BaseRequest<TResponse>> Post<TResponse>(string uri)
        {
            var restRequest = BodyRequest(uri, Method.Post);
            return await GetContent<TResponse>(restRequest);
        }

        public async Task<BaseRequest<TResponse>> Put<TResponse, TBody>(string uri, TBody body)
        {
            var restRequest = BodyRequest(uri, body, Method.Put);
            return await GetContent<TResponse>(restRequest);
        }

        public async Task<BaseRequest<TResponse>> Put<TResponse>(string uri)
        {
            var restRequest = Request(uri, Method.Put);
            return await GetContent<TResponse>(restRequest);
        }

        public async Task<BaseRequest<bool>> Put(string uri)
        {
            var restRequest = Request(uri, Method.Put);
            return await GetNoContent(restRequest);
        }

        public async Task<BaseRequest<bool>> Put<TBody>(string uri, TBody body)
        {
            var restRequest = BodyRequest(uri, body, Method.Put);
            return await GetNoContent(restRequest);
        }

        public async Task<BaseRequest<TResponse>> Delete<TResponse, TBody>(string uri, TBody body)
        {
            var restRequest = BodyRequest(uri, body, Method.Delete);
            return await GetContent<TResponse>(restRequest);
        }

        public async Task<BaseRequest<TResponse>> Delete<TResponse>(string uri)
        {
            var restRequest = BodyRequest(uri, Method.Delete);
            return await GetContent<TResponse>(restRequest);
        }

        public async Task<BaseRequest<TResponse>> Patch<TResponse, TBody>(string uri, TBody body)
        {
            var restRequest = BodyRequest(uri, body, Method.Patch);
            return await GetContent<TResponse>(restRequest);
        }

        public async Task<BaseRequest<TResponse>> Patch<TResponse>(string uri)
        {
            var restRequest = BodyRequest(uri, Method.Patch);
            return await GetContent<TResponse>(restRequest);
        }

        public async Task<BaseRequest<bool>> Patch(string uri)
        {
            var restRequest = Request(uri, Method.Patch);
            return await GetNoContent(restRequest);
        }

        public async Task<BaseRequest<bool>> Patch<TBody>(string uri, TBody body)
        {
            var restRequest = BodyRequest(uri, body, Method.Patch);
            return await GetNoContent(restRequest);
        }

        protected virtual RestRequest Request(string uri, Method method)
        {
            SetupClient();
            var url = $"{uri}&appid={AppSettings.Apikey}";
            var restRequest = new RestRequest(url, method);

            return restRequest;
        }

        protected virtual RestRequest BodyRequest<T>(string uri, T body, Method method)
        {
            SetupClient();
            var url = $"{uri}&appid={AppSettings.Apikey}";
            var restRequest = new RestRequest(url, method);

            if (body != null)
                restRequest.AddJsonBody<object>(body);
            return restRequest;
        }

        protected virtual RestRequest BodyRequest(string uri, Method method)
        {
            SetupClient();
            var url = $"{uri}&appid={AppSettings.Apikey}";
            var restRequest = new RestRequest(url, method);

            return restRequest;
        }

        private async Task<BaseRequest<TResponse>> GetContent<TResponse>(RestRequest restRequest)
        {
            RestClient.UseNewtonsoftJson();
            var response = await RestClient.ExecuteAsync<TResponse>(restRequest);
            var result = GetResult<TResponse>(response);
            if (response.IsSuccessful)
                result.Response = response.Data;
            return result;
        }

        private async Task<BaseRequest<bool>> GetNoContent(RestRequest restRequest)
        {
            var response = await RestClient.ExecuteAsync(restRequest);
            var result = GetResult<bool>(response);

            return result;
        }

        private BaseRequest<TResponse> GetResult<TResponse>(RestResponse response)
        {
            var result = new BaseRequest<TResponse>
            {
                StatusCode = response.StatusCode,
                Request = response.Request,
                RestClient = RestClient
            };

            if (!response.IsSuccessful)
            {
                if (!String.IsNullOrEmpty(response.ErrorMessage))
                {
                    result.ErrorRequest = response.ErrorMessage;
                }
            }

            return result;
        }

        protected void SetupClient()
        {
            RestClientOptions.BaseUrl = AppSettings.BaseUrl;
            RestClientOptions.Expect100Continue = false;

            var proxy = new WebProxy()
            {
                Credentials = CredentialCache.DefaultCredentials
            };

            RestClientOptions.Proxy = proxy;

            RestClient = new RestClient(RestClientOptions);
            RestClient.UseNewtonsoftJson();
        }


        public async Task<BaseRequest<bool>> Post(string uri)
        {
            var restRequest = Request(uri, Method.Post);
            return await GetNoContent(restRequest);
        }

        public async Task<BaseRequest<bool>> Post<TBody>(string uri, TBody body)
        {
            var restRequest = BodyRequest(uri, body, Method.Post);
            return await GetNoContent(restRequest);
        }

        public async Task<BaseRequest<bool>> Delete(string uri)
        {
            var restRequest = Request(uri, Method.Delete);
            return await GetNoContent(restRequest);
        }

        public async Task<BaseRequest<bool>> Delete<TBody>(string uri, TBody body)
        {
            var restRequest = BodyRequest(uri, body, Method.Delete);
            return await GetNoContent(restRequest);
        }
    }

}