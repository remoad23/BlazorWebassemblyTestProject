using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using BlazorWebassemblyWebAPI.Repository;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BlazorWebassemblyTestProject
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            var baseAdress = new Uri("https://localhost:5001/");
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});

            builder.Services.AddHttpClient<CheckListHttpClient>(client => client.BaseAddress = baseAdress); 
            builder.Services.AddHttpClient<EntryHttpClient>(client => client.BaseAddress = baseAdress);

            builder.Services.AddOidcAuthentication(options =>
            {
                // Configure your authentication provider options here.
                // For more information, see https://aka.ms/blazor-standalone-auth
                builder.Configuration.Bind("Local", options.ProviderOptions);
            });

            await builder.Build().RunAsync();
        }
    }
}