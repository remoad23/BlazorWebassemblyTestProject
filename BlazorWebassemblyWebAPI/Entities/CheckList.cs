using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlazorTestProject.Entities
{
    public class CheckList
    {
        [Key]
        public Guid Id { get; set; }
        public string CheckListName { get; set; }
        
        
        public List<Entry> Entries { get; set; }
    }
}