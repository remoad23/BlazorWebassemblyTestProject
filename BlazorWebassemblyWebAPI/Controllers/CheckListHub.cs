using System.Linq;
using System.Threading.Tasks;
using BlazorTestProject.Entities;
using Microsoft.AspNetCore.SignalR;

namespace BlazorWebassemblyWebAPI.Controllers
{
    public class CheckListHub : Hub
    {

        private BlazorContext Context;
        
        public CheckListHub(BlazorContext context)
        {
            Context = context;
        }
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task GetAllCheckLists()
        {
            await  Clients.Caller.SendAsync("ReceiveAllCheckLists",  Context.CheckLists.ToList());
        }
        
        public async Task GetAllEntries()
        {
            await Clients.Caller.SendAsync("ReceiveAllEntries",  Context.Entries.ToList());
        }
    }
}