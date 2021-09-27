using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BlazorWASMProject.Models;

namespace BlazorWASMProject.Entities
{
    public class CheckList
    {
        [Key]
        public Guid Id { get; set; }
        public string CheckListName { get; set; }
        
        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }

        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }
        public List<Entry> Entries { get; set; }
    }
}