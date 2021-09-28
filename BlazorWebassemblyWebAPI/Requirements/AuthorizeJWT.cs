using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Configuration;
using BlazorWebassemblyWebAPI.Entities;
using BlazorWebassemblyWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace BlazorWebassemblyWebAPI.Requirements
{
    public class AuthorizeJwt : AuthorizationHandler<AuthorizeJwt>,IAuthorizationRequirement
    {
        public AuthorizeJwt()
        {
            
        }


        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthorizeJwt requirement)
        {
            var httpcontext = context.Resource as HttpContext;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SecretKeyWhichIsNotReallySecret"));

            bool validated = true;
            
            var jwt = httpcontext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();


            try
            {
                tokenHandler.ValidateToken(jwt, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = false,
                    ValidateLifetime = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidIssuer = "BlazorWASM", 
                    ValidAudience = "https://localhost:5010", 
                }, out SecurityToken validatedToken);
            }
            catch(SecurityTokenException)
            {
                validated =  false; 
            }
            catch(Exception e)
            { 
                Console.WriteLine(e.ToString());
                throw;
            }


            
            if (validated)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
            
           //  var jwtToken = (JwtSecurityToken) validatedToken;

            


            return Task.CompletedTask;
        }
        
    }
}