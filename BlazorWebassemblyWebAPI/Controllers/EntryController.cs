using System;
using System.Collections.Generic;
using System.Linq;
using BlazorTestProject.Entities;
using CheckListLibrary;
using CheckListLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWebassemblyWebAPI.Controllers
{
    public class EntryController : Controller
    {
        private IGenericRepository<Entry> EntryRepository;
        public EntryController(IGenericRepository<Entry> entry1Repository)
        {
            EntryRepository = entry1Repository;
        }

        [HttpGet]
        [Route("API/GetAllEntries")]
        public ActionResult<List<Entry>> GetAllEntries()
        {
            return EntryRepository.GetAll().ToList();
        }
        
        [HttpGet]
        [Route("API/GetEntry")]
        public ActionResult<Entry> GetEntry(string entryId)
        {
            return EntryRepository
                .Find(e => e.Id.Equals(entryId.ToString()) )
                .FirstOrDefault();
        }
        
        [HttpPost]
        [Route("API/CreateEntry")]
        public ActionResult<Entry> CreateEntry([FromBody]Entry entry)
        {
            EntryRepository.Add(entry);
            EntryRepository.Complete();
            return entry;
        }
        
        [HttpDelete]
        [Route("API/DeleteEntry/{entryId}")]
        public ActionResult DeleteEntry(Guid entryId)
        {
            var entry = EntryRepository
                .Find(e => e.Id.Equals(entryId.ToString()) )
                .FirstOrDefault();
            EntryRepository.Remove(entry);
            EntryRepository.Complete();
            return Ok();
        }
    }
}