using System;
using System.Collections.Generic;
using System.Linq;
using BlazorTestProject.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BlazorWebassemblyWebAPI.Controllers
{
    public class CheckListController : Controller
    {

        private BlazorContext Context;
        public CheckListController(BlazorContext context)
        {
            Context = context;
        }
        
        [HttpGet]
        [Route("API/GetCheckList")]
        public ActionResult<CheckList> GetCheckList(Guid checkListId)
        {
            return Context.CheckLists.Find(checkListId);
        }
        
        [HttpGet]
        [Route("API/GetAllCheckLists")]
        public ActionResult< List<Tuple<CheckList,List<Entry>>> > GetAllCheckLists()
        {
            List<Tuple<CheckList,List<Entry>>> checkListsToPass = new List<Tuple<CheckList,List<Entry>>>();
            List<CheckList> checklists = Context.CheckLists.ToList();
            foreach(CheckList checklist in checklists)
            {
                var entry = Context.Entries.Where(e => e.CheckListId.Equals(checklist.Id)).ToList();
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
            Context.CheckLists.Add(checklist);
            Context.SaveChanges();
            return checklist;
        }
        
        [HttpDelete]
        [Route("API/DeleteCheckList/{checkListId}")]
        public ActionResult DeleteCheckList(Guid checkListId)
        {
            var checklist = Context.CheckLists.Find(checkListId);
            Context.CheckLists.Remove(checklist);
            Context.SaveChanges();
            return Ok();
        }
        

    }
}