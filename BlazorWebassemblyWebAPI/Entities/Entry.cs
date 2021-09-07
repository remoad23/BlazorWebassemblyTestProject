using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorTestProject.Entities
{
    public class Entry
    {
        [Key]
        public Guid Id { get; set; }
        public string EntryText { get; set; }
        
        public Guid CheckListId { get; set; }
        public bool IsDone { get; set; }
        
        public CheckList CheckList { get; set; }
    }
}