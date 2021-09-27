using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BlazorWASMProject.Models;
using BlazorWASMProject.Services.Interfaces;

namespace BlazorWASMProject.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private ILocalStorageService LocalStorageService;
        
        private string url = "https://localhost:5001/";
        
        public AuthService(HttpClient httpClient,ILocalStorageService localStorageService)
        {
            LocalStorageService = localStorageService;
            _httpClient = httpClient;
        }
        public async Task<CurrentUser> CurrentUserInfo()
        {
            var authHeader = _httpClient.DefaultRequestHeaders.Authorization;
            var token = await LocalStorageService.GetLocalStorage("authToken");
            if(token is not null)
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            
            var result = await _httpClient.GetFromJsonAsync<CurrentUser>(url + "api/auth/currentuserinfo");
            return result;
    
        }

        public async Task Login(LoginRequest loginRequest)
        {
            var response = await _httpClient.PostAsJsonAsync(url +"api/auth/login", loginRequest);
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await response.Content.ReadAsStringAsync());
            
            var authContent = await response.Content.ReadAsStringAsync();
            LoginResponse result = JsonSerializer.Deserialize<LoginResponse>(authContent,new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });

            LocalStorageService.UpdateLocalStorage("authToken", result.Token);
            
      //    ((AuthStateProvider)_authStateProvider).NotifyUserAuthentication(userForAuthentication.Email);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);
      //    return new AuthResponseDto { IsAuthSuccessful = true };
            
            response.EnsureSuccessStatusCode();
        }
        public async Task Logout()
        {
        //    var result = await _httpClient.PostAsync(url +"api/auth/logout", null);
        //    result.EnsureSuccessStatusCode();
            LocalStorageService.ClearLocalStorage("authToken");
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
        public async Task Register(RegisterRequest registerRequest)
        {
            var result = await _httpClient.PostAsJsonAsync(url +"api/auth/register", registerRequest);
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
            result.EnsureSuccessStatusCode();
        }
    }
}