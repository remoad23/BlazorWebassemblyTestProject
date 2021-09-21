using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BlazorWASMProject.Models;
using BlazorWASMProject.Services.Interfaces;

namespace BlazorWASMProject.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;

        private string url = "https://localhost:5001/";
        
        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<CurrentUser> CurrentUserInfo()
        {
            var result = await _httpClient.GetFromJsonAsync<CurrentUser>(url +"api/auth/currentuserinfo");
            return result;
        }
        public async Task Login(LoginRequest loginRequest)
        {
            var result = await _httpClient.PostAsJsonAsync(url +"api/auth/login", loginRequest);
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
            result.EnsureSuccessStatusCode();
        }
        public async Task Logout()
        {
            var result = await _httpClient.PostAsync(url +"api/auth/logout", null);
            result.EnsureSuccessStatusCode();
        }
        public async Task Register(RegisterRequest registerRequest)
        {
            Console.WriteLine("RegisterReq:  "+registerRequest.UserName);
            var result = await _httpClient.PostAsJsonAsync(url +"api/auth/register", registerRequest);
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
            result.EnsureSuccessStatusCode();
        }
    }
}