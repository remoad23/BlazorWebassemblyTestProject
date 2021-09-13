using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BlazorWASMProject.Entities;
using CheckListLibrary.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace BlazorWASMProject.Repository
{
    public class GenericRepository<T> : IGenericRepository<T>  where T : class
    {
        private readonly HttpClient Http;
        private readonly IConfiguration Configuration;
        private string Url;
        private string TypeName;
        
        public GenericRepository(HttpClient http,IConfiguration configuration)
        {
            Http = http;
            Configuration = configuration;
            Url = Configuration["RepositorySettings:URL"];
            TypeName = typeof(T).Name;
        }
        
        
        public T GetById(int id)
        {
            return Task.Run( () => this.Http.GetFromJsonAsync<T>($"{Url}/Get{TypeName}/{id}")).Result;
        }

        public IEnumerable<T> GetAll(bool withEntity = false,Expression<Func<Object, Object>> expression = null)
        {
            return Task.Run( async () => await this.Http.GetFromJsonAsync<List<T>>($"{Url}/GetAll{TypeName}")).Result;
        }
        
        
        public void Add(T entity)
        {
            Task.Run( () => this.Http.PostAsJsonAsync<T>($"{Url}/Create{TypeName}",entity));
        }
        
        public void Remove(T entity)
        {
            #nullable enable
            var id = entity.GetType()?.GetProperty("Id")?.GetValue(entity, null);
            if(id is string)
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