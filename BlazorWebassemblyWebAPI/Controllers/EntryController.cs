using System;
using System.Collections.Generic;
using System.Linq;
using BlazorTestProject.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWebassemblyWebAPI.Controllers
{
    public class EntryController : Controller
    {
        
        private BlazorContext Context;
        public EntryController(BlazorContext context)
        {
            Context = context;
        }

        [HttpGet]
        [Route("API/GetAllEntries")]
        public ActionResult<List<Entry>> GetAllEntries()
        {
            return Context.Entries.ToList();
        }
        
        [HttpGet]
        [Route("API/GetEntry")]
        public ActionResult<Entry> GetEntry(string entryId)
        {
            return Context.Entries.Find(entryId);
        }
        
        [HttpPost]
        [Route("API/CreateEntry")]
        public ActionResult<Entry> CreateEntry(Guid checkListId,string text)
        {
            var entry = new Entry
            {
                Id = Guid.NewGuid(),
                IsDone = false,
                CheckListId = checkListId,
                EntryText = text
            };
            Context.Entries.Add(entry);

            Context.SaveChanges();
            return entry;
        }
        
        [HttpDelete]
        [Route("API/DeleteEntry/{entryId}")]
        public ActionResult DeleteEntry(string entryId)
        {
            var entry = Context.Entries.Find(entryId);
            Context.Entries.Remove(entry);
            Context.SaveChanges();
            return Ok();
        }
    }
}