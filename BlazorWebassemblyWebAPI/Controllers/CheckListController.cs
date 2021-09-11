using System;
using System.Collections.Generic;
using System.Linq;
using BlazorTestProject.Entities;
using CheckListLibrary;
using CheckListLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BlazorWebassemblyWebAPI.Controllers
{
    public class CheckListController : Controller
    {
        private IGenericRepository<CheckList> CheckListRepository;
        private IGenericRepository<Entry> EntryRepository;
        
        public CheckListController(BlazorContext context,
            IGenericRepository<CheckList> checkListRepository,
            IGenericRepository<Entry> entryRepository)
        {
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
        [Route("API/GetAllCheckLists")]
        public ActionResult< List<Tuple<CheckList,List<Entry>>> > GetAllCheckLists()
        {
            List<Tuple<CheckList,List<Entry>>> checkListsToPass = new List<Tuple<CheckList,List<Entry>>>();
            List<CheckList> checklists = CheckListRepository.GetAll().ToList();
            foreach(CheckList checklist in checklists)
            {
                var entry = EntryRepository
                    .Find(e => e.CheckListId.Equals(checklist.Id))
                    .ToList();
                checkListsToPass.Add(new Tuple<CheckList,List<Entry>>(checklist,entry));
            }
            return checkListsToPass;
        }
        
        [HttpPost]
        [Route("API/CreateCheckList")]
        public ActionResult<CheckList> CreateCheckList([FromBody]string checkListName)
        {
            var checklist = new CheckList
            {
                Id = Guid.NewGuid(),
                CheckListName = checkListName
            };
            CheckListRepository.Add(checklist);
            CheckListRepository.Complete();
            return checklist;
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