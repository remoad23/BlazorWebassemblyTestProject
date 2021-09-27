using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BlazorWebassemblyWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BlazorTestProject.Entities;
using BlazorWebassemblyWebAPI.Services;
using BlazorWebassemblyWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace BlazorWebassemblyWebAPI.Controllers
{

    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private IJwtService JwtService;
        private IJwtParser JwtParser;
        private UserManager<ApplicationUser> Usermanager;
        
        public AuthController(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager,
            IJwtService jwtService,
            IJwtParser jwtParser,
            UserManager<ApplicationUser> usermanager)
        {
            Usermanager = usermanager;
            JwtParser = jwtParser;
            _userManager = userManager;
            _signInManager = signInManager;
            JwtService = jwtService;
        }
        
        [Route("api/auth/login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody]LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
                return Unauthorized();

            return Ok(new LoginResponse
            {
                Sucess = true,
                Token = JwtService.GenerateJwtToken(user)
            });
        }
        
        [Route("api/auth/register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody]RegisterRequest parameters)
        {
            var user = new ApplicationUser();
            user.UserName = parameters.UserName;
            var result = await _userManager.CreateAsync(user, parameters.Password);
            if (!result.Succeeded) return BadRequest(result.Errors.FirstOrDefault()?.Description);
            return await Login(new LoginRequest
            {
                UserName = parameters.UserName,
                Password = parameters.Password
            });
        }
        
        [Route("api/auth/logout")]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }
        
        [Route("api/auth/currentuserinfo")]
        [HttpGet]
        public async Task<ActionResult<CurrentUser>> CurrentUserInfo()
        {
            var token =  Request.Headers["Authorization"];
            Console.WriteLine("token:   " + token);
            List<Claim> claims = JwtParser.ParseClaimsFromJwt(token).ToList();
            var nameClaim = claims.Single(c => c.Type.Equals(ClaimTypes.Name)); //.Where(c => c.Type.Equals(ClaimTypes.Name)).FirstOrDefault();
            ApplicationUser queriedUser = await Usermanager.FindByNameAsync(nameClaim.Value);
            
            Console.WriteLine("user "+(queriedUser != null).ToString());
            var currentUser = new CurrentUser
            {
                IsAuthenticated = queriedUser != null,
                UserName = nameClaim.Value ?? "",
                Claims = claims?.ToDictionary(c => c.Type, c => c.Value) 
            };

            return Ok(currentUser);
        }
    }
}