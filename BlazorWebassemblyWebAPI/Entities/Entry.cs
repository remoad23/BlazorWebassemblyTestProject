using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace BlazorTestProject.Entities
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