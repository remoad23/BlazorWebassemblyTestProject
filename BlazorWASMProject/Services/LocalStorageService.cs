using System.Threading.Tasks;
using BlazorWASMProject.Services.Interfaces;
using Blazored.LocalStorage;
using ILocalStorageService  = Blazored.LocalStorage.ILocalStorageService;


namespace BlazorWASMProject.Services
{
    public class LocalStorageService : BlazorWASMProject.Services.Interfaces.ILocalStorageService
    {
        private readonly ILocalStorageService LocalStorage;
        
        public LocalStorageService(ILocalStorageService localstorage)
        {
            LocalStorage = localstorage;
        }
        
        public async Task<dynamic> GetLocalStorage(string noteKey)
        {
            return await LocalStorage.GetItemAsync<string>(noteKey);
        }
        
        public async void UpdateLocalStorage(string key,dynamic value)
        {
            await LocalStorage.SetItemAsync(key, value);
        }

        public async void ClearLocalStorage(string key)
        {
            await LocalStorage.RemoveItemAsync(key);
        }
        
        public async void ClearAllLocalStorage(string key)
        {
            await LocalStorage.ClearAsync();
        }
    }
}