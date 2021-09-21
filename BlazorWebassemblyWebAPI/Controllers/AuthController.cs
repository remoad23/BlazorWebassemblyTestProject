using System;
using System.Linq;
using System.Threading.Tasks;
using BlazorWebassemblyWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlazorWebassemblyWebAPI.Controllers
{

    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        
        [Route("api/auth/login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody]LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName).ConfigureAwait(false);
            if (user == null) return BadRequest("User does not exist");
            var singInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!singInResult.Succeeded) return BadRequest("Invalid password");
            Console.WriteLine("Succeeded:  "+ singInResult.Succeeded);
            await _signInManager.SignInAsync(user, request.RememberMe).ConfigureAwait(false);
            Console.WriteLine("IdentityVal:  "+_signInManager.IsSignedIn(User));
            Console.WriteLine(Request.Cookies.Count());
            return Ok();
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
        public ActionResult<CurrentUser> CurrentUserInfo()
        {
            var currentUser = new CurrentUser
            {
                IsAuthenticated = User.Identity.IsAuthenticated,
                UserName = User.Identity.Name,
                Claims = User.Claims
                    .ToDictionary(c => c.Type, c => c.Value)
            };
            return Ok(currentUser);
        }
    }
}