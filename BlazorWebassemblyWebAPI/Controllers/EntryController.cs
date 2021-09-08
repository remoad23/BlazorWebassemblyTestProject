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
        public ActionResult<Entry> CreateEntry([FromBody]Entry entry)
        {
            Context.Entries.Add(entry);
            Context.SaveChanges();
            return entry;
        }
        
        [HttpDelete]
        [Route("API/DeleteEntry/{entryId}")]
        public ActionResult DeleteEntry(Guid entryId)
        {
            var entry = Context.Entries.Find(entryId);
            Context.Entries.Remove(entry);
            Context.SaveChanges();
            return Ok();
        }
    }
}