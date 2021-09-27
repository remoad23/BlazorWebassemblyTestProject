using BlazorTestProject.Entities;
using BlazorWebassemblyWebAPI.Entities;
using CheckListLibrary;

namespace BlazorWebassemblyWebAPI.Repositories
{
    public class BlazorRepository<T> : GenericRepository<T,BlazorContext> where T : class
    {
        public BlazorRepository(BlazorContext context) : base(context)
        {
            
        }
    }
}