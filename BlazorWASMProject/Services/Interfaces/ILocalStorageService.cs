using System;
using System.Threading.Tasks;

namespace BlazorWASMProject.Services.Interfaces
{
    public interface ILocalStorageService
    {
        public async Task<dynamic> GetLocalStorage(string noteKey) => throw new NotImplementedException();
   
        public async void UpdateLocalStorage(string key,dynamic value) => throw new NotImplementedException();
        public async void ClearLocalStorage(string key) => throw new NotImplementedException();
        public async void ClearAllLocalStorage(string key) => throw new NotImplementedException();
    }
}