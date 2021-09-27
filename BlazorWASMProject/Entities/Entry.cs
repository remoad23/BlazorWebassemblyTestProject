using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace BlazorWASMProject.Entities
{
    public class Entry
    {
        [Key]
        public Guid Id { get; set; }
        public string EntryText { get; set; }
        
        public Guid CheckListId { get; set; }
        public bool IsDone { get; set; }
        
        [IgnoreDataMember]
        public CheckList CheckList { get; set; }
    }
}