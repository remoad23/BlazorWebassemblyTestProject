﻿using BlazorTestProject.Entities;
using BlazorWebassemblyWebAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlazorWebassemblyWebAPI.Entities
{
    public class BlazorContext : IdentityDbContext<ApplicationUser>
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