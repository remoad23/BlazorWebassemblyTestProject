using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BlazorWebassemblyWebAPI.Models;
using BlazorWebassemblyWebAPI.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BlazorWebassemblyWebAPI.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration Configuration;
        
        public JwtService(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public SigningCredentials GetSigningCredentials() 
        { 
            var _jwtSettings = Configuration.GetSection("JWTSettings"); 
            var key = Encoding.UTF8.GetBytes(_jwtSettings["securityKey"]); 
            var secret = new SymmetricSecurityKey(key); 
            
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256); 
        }
        
        public List<Claim> GetClaims(ApplicationUser user) 
        { 
            var claims = new List<Claim> 
            { 
                new Claim(ClaimTypes.Name, user.Email) 
            }; 
            
            return claims; 
        }
        
        public JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims) 
        { 
            var _jwtSettings = Configuration.GetSection("JWTSettings"); 
            var tokenOptions = new JwtSecurityToken(
                issuer: _jwtSettings["validIssuer"], 
                audience: _jwtSettings["validAudience"], 
                claims: claims, 
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtSettings["expiryInMinutes"])), 
                signingCredentials: signingCredentials); 
            
            return tokenOptions; 
        }

        public dynamic GenerateJwtToken(List<Claim> userClaims)
        {
            var signingCredentials = GetSigningCredentials();
            var tokenOptions = GenerateTokenOptions(signingCredentials, userClaims);
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }
    }
}