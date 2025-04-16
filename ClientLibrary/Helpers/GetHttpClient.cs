using BasicLibrary.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientLibrary.Helpers
{
    public class GetHttpClient (IHttpClientFactory httpClientFactory, LocalStorageService localStorageService)
    {
        private const string HeaderKey = "Authorization";
        public async Task<HttpClient> GetPrivateHttpClient()
        {
            var client = httpClientFactory.CreateClient("BlazorApiClient");
            var stringToken = await localStorageService.GetToken();
            if (string.IsNullOrEmpty(stringToken)) return client;

            var deserializeToken = Serialization.DeserializeJsonString<UserSession>(stringToken);
            if (deserializeToken == null) return client;

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",deserializeToken.Token);
            return client;
        }


        public HttpClient GetPublicHttpClient()
        {
            var client = httpClientFactory.CreateClient("BlazorApiClient");
            client.DefaultRequestHeaders.Remove(HeaderKey);
            return client;
        }

    }


}
