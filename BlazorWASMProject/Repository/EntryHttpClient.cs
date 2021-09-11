using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BlazorWASMProject.Entities;

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
        
        public async Task CreateEntry(Entry _entry)
        {
            var entry = await this.Http.PostAsJsonAsync<Entry>("API/CreateEntry",_entry);
        }
        
        public async Task DeleteEntry(Entry entry)
        {
            await this.Http.DeleteAsync($"API/DeleteEntry/{entry.Id}");
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