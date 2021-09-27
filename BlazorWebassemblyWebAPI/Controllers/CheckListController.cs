using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BlazorTestProject.Entities;
using BlazorWebassemblyWebAPI.Entities;
using BlazorWebassemblyWebAPI.Services;
using BlazorWebassemblyWebAPI.Services.Interfaces;
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
        private IJwtParser JwtParser;
        
        public CheckListController(BlazorContext context,
            IGenericRepository<CheckList> checkListRepository,
            IGenericRepository<Entry> entryRepository,IJwtParser jwtParser)
        {
            JwtParser = jwtParser;
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
        public async Task<ActionResult<IEnumerable<CheckList>>> GetAllCheckLists()
        {
            string token =  Request.Headers["Authorization"];
            List<Claim> claims = JwtParser.ParseClaimsFromJwt(token).ToList();
            string id = claims.Single(c => c.Type.Equals(ClaimTypes.NameIdentifier)).Value;
            Console.WriteLine(id);
            var checkLists = await CheckListRepository.GetAll(entityToInclude: "Entries",(ch =>ch.ApplicationUserId.Equals(id)) ); //.Find(e => e.ApplicationUserId.Equals(id)).in;
            Console.WriteLine(checkLists.Count());
      //      IEnumerable<CheckList> checklists = CheckListRepository.GetAll(entityToInclude:"Entries").Result;
            return Ok(checkLists);
        }
        
        [HttpPost]
        [Route("API/CreateCheckList")]
        public ActionResult<CheckList> CreateCheckList([FromBody]CheckList checkList)
        {
            string token =  Request.Headers["Authorization"];
            List<Claim> claims = JwtParser.ParseClaimsFromJwt(token).ToList();
            string id = claims.Single(c => c.Type.Equals(ClaimTypes.NameIdentifier)).Value;
            checkList.ApplicationUserId = id;
            CheckListRepository.Add(checkList);
            CheckListRepository.Complete();
            return checkList;
        }
        
        [HttpDelete]
        [Route("API/DeleteCheckList/{checkListId}")]
        public ActionResult DeleteCheckList(Guid checkListId)
        {
            Console.WriteLine("???");
            var checkList = CheckListRepository
                .Find(ch => ch.Id.Equals(checkListId))
                .FirstOrDefault();
            CheckListRepository.Remove(checkList);
            CheckListRepository.Complete();
            return Ok();
        }
        

    }
}