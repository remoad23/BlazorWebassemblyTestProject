using System;
using System.Collections.Generic;
using System.Linq;
using BlazorTestProject.Entities;
using CheckListLibrary;
using CheckListLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BlazorWebassemblyWebAPI.Controllers
{
    public class CheckListController : Controller
    {
        private IGenericRepository<CheckList> CheckListRepository;
        private IGenericRepository<Entry> EntryRepository;
        private BlazorContext Context;
        
        public CheckListController(BlazorContext context,
            IGenericRepository<CheckList> checkListRepository,
            IGenericRepository<Entry> entryRepository)
        {
            Context = context;
            EntryRepository = entryRepository;
            CheckListRepository = checkListRepository;
        }
        
        [HttpGet]
        [Route("API/GetCheckList")]
        public ActionResult<CheckList> GetCheckList(Guid checkListId)
        {
            return CheckListRepository
                .Find(ch => ch.Id.Equals(checkListId.ToString()))
                .FirstOrDefault();
        }
        
        [HttpGet]
        [Route("API/GetAllCheckList")]
        public ActionResult<IEnumerable<CheckList>> GetAllCheckLists()
        {
            IEnumerable<CheckList> checklists = CheckListRepository.GetAll(entityToInclude:"Entries").Result;
            return Ok(checklists);
        }
        
        [HttpPost]
        [Route("API/CreateCheckList")]
        public ActionResult<CheckList> CreateCheckList([FromBody]CheckList checkList)
        {
            CheckListRepository.Add(checkList);
            CheckListRepository.Complete();
            return checkList;
        }
        
        [HttpDelete]
        [Route("API/DeleteCheckList/{checkListId}")]
        public ActionResult DeleteCheckList(Guid checkListId)
        {
            var checkList = CheckListRepository
                .Find(ch => ch.Id.Equals(checkListId.ToString()))
                .FirstOrDefault();
            CheckListRepository.Remove(checkList);
            CheckListRepository.Complete();
            return Ok();
        }
        

    }
}