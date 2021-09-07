using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BlazorWebassemblyTestProject.Entities;

namespace BlazorWebassemblyWebAPI.Repository
{
    public class EntryHttpClient
    {
        private readonly HttpClient Http;

        public EntryHttpClient(HttpClient http)
        {
            Http = http;
        }

        public async Task<Entry> GetEntry(Guid entryId)
        {
            return await this.Http.GetFromJsonAsync<Entry>("API/GetEntry");
        }
        
        public async Task<List<Entry>> GetAllEntries()
        {
            return await this.Http.GetFromJsonAsync<List<Entry>>("API/GetAllEntries");
        }
        
        public async Task<Entry> CreateEntry(string entryText)
        {
            var entry = await this.Http.PostAsJsonAsync<string>("API/CreateEntry",entryText);
            return new Entry();
        }
        
        public async Task DeleteEntry(string entryId)
        {
            await this.Http.DeleteAsync($"API/DeleteEntry/{entryId}");
        }
        
        /*
        public async Task<List<Entry>> CreateEntries(string[] entriesTexts)
        {
            return await this.Http.PostAsJsonAsync<List<Entry>>("api/survey");
        }
        
        public async Task<List<Entry>> DeleteEntries(Guid[] entryIds)
        {
            return await this.Http.DeleteAsync<List<Entry>>("api/survey");
        } */
    }
}