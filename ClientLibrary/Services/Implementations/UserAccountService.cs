using BasicLibrary;
using BasicLibrary.Dto;
using BasicLibrary.Responses;
using ClientLibrary.Helpers;
using ClientLibrary.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;


using System.Text;
using System.Threading.Tasks;

namespace ClientLibrary.Services.Implementations
{
    public class UserAccountService(GetHttpClient getHttpClient) : IUserAccountService

    {

        public const string AuthUrl = "https://localhost:7103/api/Authentification";
        public async Task<GeneralResponse> CreateAsync(Register user)
        {
            var httpClient = getHttpClient.GetPublicHttpClient();
            var res = await httpClient.PostAsJsonAsync($"{AuthUrl}/register", user);
            if (!res.IsSuccessStatusCode) return new GeneralResponse(false, "Error occured");
            return await res.Content.ReadFromJsonAsync<GeneralResponse>()!;

        }
        public async Task<LoginResponse> SignInAsync(Login user)
        {
            var httpClient = getHttpClient.GetPublicHttpClient();
           
                var res = await httpClient.PostAsJsonAsync($"{AuthUrl}/login", user);
                if (!res.IsSuccessStatusCode) return new LoginResponse(false, "Error occured");
            
                return await res.Content.ReadFromJsonAsync<LoginResponse>()!;

        }

        public async  Task<WeatherForecast[]> GetWeatherForecasts()
        {
            var httpClient =await  getHttpClient.GetPrivateHttpClient();
            var res = await httpClient.GetFromJsonAsync<WeatherForecast[]>("api/weatherforecast");
            return res!;

        }

        public async Task<LoginResponse> RefreshTokenAsync(RefreshToken user)
        {
            throw new NotImplementedException();

        }

       
    }
}
