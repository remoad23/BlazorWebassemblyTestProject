using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BlazorWebassemblyWebAPI.Models;
using Microsoft.IdentityModel.Tokens;

namespace BlazorWebassemblyWebAPI.Services.Interfaces
{
    public interface IJwtService
    {
        SigningCredentials GetSigningCredentials();
        
        List<Claim> GetClaims(ApplicationUser user);
        
        JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims);
        
        dynamic GenerateJwtToken(List<Claim> userClaims);

    }
}