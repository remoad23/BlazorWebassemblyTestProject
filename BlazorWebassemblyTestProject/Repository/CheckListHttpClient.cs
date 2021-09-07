using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BlazorWebassemblyTestProject.Entities;

namespace BlazorWebassemblyWebAPI.Repository
{
    public class CheckListHttpClient
    {
        private readonly HttpClient Http;

        public CheckListHttpClient(HttpClient http)
        {
            Http = http;
        }

        public async Task< List<Tuple<CheckList,List<Entry>>> > GetAllCheckLists()
        {
            return await this.Http.GetFromJsonAsync< List<Tuple<CheckList,List<Entry>>> >("API/GetAllCheckLists");
        }
        
        public async Task<CheckList> GetCheckList(Guid checkListId)
        {
            return await this.Http.GetFromJsonAsync<CheckList>("API/GetCheckList");
        }

        public async Task<CheckList> CreateCheckList(string checkListName)
        {
            var checkList = await this.Http.PostAsJsonAsync<string>("API/CreateCheckList",checkListName);
            return new CheckList();
        }
        
        public async Task DeleteCheckList(Guid checkList)
        {
            await this.Http.DeleteAsync("API/DeleteCheckList");
        }
    }
}