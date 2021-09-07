using Microsoft.EntityFrameworkCore;

namespace BlazorTestProject.Entities
{
    public class BlazorContext : DbContext
    {
        public DbSet<CheckList> CheckLists { get; set; }
        public DbSet<Entry> Entries { get; set; }
        
        public BlazorContext(DbContextOptions<BlazorContext> options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}