using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BlazorWASMProject.Entities;
using BlazorWASMProject.Services;
using BlazorWASMProject.Services.Interfaces;
using CheckListLibrary.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BlazorWASMProject.Repository
{
    public class GenericRepository<T> : IGenericRepository<T>  where T : class
    {
        private readonly HttpClient Http;
        private readonly IConfiguration Configuration;
        private string Url;
        private string TypeName;
        private ILocalStorageService LocalStorageService;
        
        public GenericRepository(HttpClient http,IConfiguration configuration,ILocalStorageService localStorageService)
        {
            Http = http;
            Configuration = configuration;
            Url = Configuration["RepositorySettings:URL"];
            TypeName = typeof(T).Name;
            Init();
        }
        
        
        private async Task Init()
        {
            await InsertJwtTokenIntoHttpClient();
        }

        private async Task InsertJwtTokenIntoHttpClient()
        {
            var token = (string)( await LocalStorageService.GetLocalStorage("authToken") );
            if(token is not null && !token.Equals(""))
                Http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
        }
        
        public T GetById(int id)
        {
            return Task.Run( () => this.Http.GetFromJsonAsync<T>($"{Url}/Get{TypeName}/{id}")).Result;
        }

        #nullable  enable
        public async Task<IEnumerable<T>> GetAll(string entityToInclude = "",Expression<Func<T, bool>>? expression = null)
        {
            var result = await this.Http.GetFromJsonAsync<IEnumerable<T>>($"{Url}/GetAll{TypeName}")
                 .ConfigureAwait(continueOnCapturedContext: false);
                
             return  result;
        }
        #nullable disable

        public void Add(T entity)
        {
            Task.Run( () => this.Http.PostAsJsonAsync<T>($"{Url}/Create{TypeName}",entity));
        }
        
        public void Remove(T entity)
        {
            #nullable enable
            var id = entity.GetType()?.GetProperty("Id")?.GetValue(entity, null);
            Task.Run( () => this.Http.DeleteAsync($"{Url}/Delete{TypeName}/{id}"));
            #nullable disable
        }
        
        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            // @TODO 
            return null;
        }
        
        public void AddRange(IEnumerable<T> entities)
        {
            // @TODO 
        }
        

        public void RemoveRange(IEnumerable<T> entities)
        {
            // @TODO 
        }

        public void Complete()
        {
            // @TODO 
        }
    }
}