using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BlazorWebassemblyWebAPI.Models;
using Microsoft.IdentityModel.Tokens;

namespace BlazorWebassemblyWebAPI.Services.Interfaces
{
    public interface IJwtService
    { 
        public dynamic GenerateJwtToken(ApplicationUser user);
        private SigningCredentials GetSigningCredentials() => throw new NotImplementedException();
        
        private List<Claim> GetClaims(ApplicationUser user) => throw new NotImplementedException();

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims) => throw new NotImplementedException();
    }
}