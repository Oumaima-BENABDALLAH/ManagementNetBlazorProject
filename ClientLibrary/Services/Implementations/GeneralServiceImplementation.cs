using BasicLibrary.Responses;
using ClientLibrary.Helpers;
using ClientLibrary.Services.Contracts;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Net.Http.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;
using BasicLibrary.Entites;
using System.Reflection;

namespace ClientLibrary.Services.Implementations
{
    public class GeneralServiceImplementation<T>(GetHttpClient getHttpClient) : IGenericServiceInterface<T>
    {
        //public const string AuthUrl = "https://localhost:7103/api/Authentification";

        //Create

        public async Task<GeneralResponse> Insert(T item, string baseUrl)
        {

            var httpClient = await getHttpClient.GetPrivateHttpClient();
            var res = await httpClient.PostAsJsonAsync($"{baseUrl}/add", item);
            var resultat = await res.Content.ReadFromJsonAsync<GeneralResponse>();
            return resultat!;

        }
        //Read All
        public async Task<List<T>> GetAll(string baseUrl)
        {
            
            var httpClient = await getHttpClient.GetPrivateHttpClient();
            var res = await httpClient.GetFromJsonAsync<List<T>>($"https://localhost:7103/api/GeneralDepartment/all");
            return res!;
        }


        //Read Single {id}

        public async Task<T> GetById(int id, string baseUrl)
        {
            var httpClient = await getHttpClient.GetPrivateHttpClient();
            var res = await httpClient.GetFromJsonAsync<T>($"{baseUrl}/single/{id}");
            return res!;
        }

        // update {model}

        public async Task<GeneralResponse> Update(T item, string baseUrl)
        {
            var httpClient = await getHttpClient.GetPrivateHttpClient();
            var response = await httpClient.PutAsJsonAsync($"https://localhost:7103/api/GeneralDepartment/update", item);
            var  res = await response.Content.ReadFromJsonAsync<GeneralResponse>();
            return res!;

        }



        // Delete {id}

        public async Task<GeneralResponse> DeleteById(int id, string baseUrl)
        {
            var httpClient = await getHttpClient.GetPrivateHttpClient();
            var response = await httpClient.DeleteAsync($"{baseUrl}/delete/{id}");
            var res = await response.Content.ReadFromJsonAsync<GeneralResponse>();
            return res!;
        }

       
    }
}
