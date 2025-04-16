using BasicLibrary.Entites;
using Blazored.LocalStorage;
using ClientLibrary.Helpers;
using ClientLibrary.Services.Contracts;
using ClientLibrary.Services.Implementations;
using ManagementNetBlazorClient;
using ManagementNetBlazorClient.ApplicationStates;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Popups;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
//BlazorApiClient
builder.Services.AddHttpClient("BlazorApiClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7207/");
}
);

//builder.Services.AddScoped(http => new HttpClient
//{
//    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
//});

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7103/") });
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<GetHttpClient>();
builder.Services.AddScoped<LocalStorageService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<IUserAccountService, UserAccountService>();


builder.Services.AddScoped<IGenericServiceInterface<GeneralDepartment>, GeneralServiceImplementation<GeneralDepartment>>();
builder.Services.AddScoped<IGenericServiceInterface<Department>, GeneralServiceImplementation<Department>>();
builder.Services.AddScoped<IGenericServiceInterface<Branch>, GeneralServiceImplementation<Branch>>();

builder.Services.AddScoped<IGenericServiceInterface<Country>, GeneralServiceImplementation<Country>>();
builder.Services.AddScoped<IGenericServiceInterface<City>, GeneralServiceImplementation<City>>();
builder.Services.AddScoped<IGenericServiceInterface<Town>, GeneralServiceImplementation<Town>>();


builder.Services.AddScoped<AllStates>();
builder.Services.AddSyncfusionBlazor();
builder.Services.AddScoped<SfDialogService>();

await builder.Build().RunAsync();
