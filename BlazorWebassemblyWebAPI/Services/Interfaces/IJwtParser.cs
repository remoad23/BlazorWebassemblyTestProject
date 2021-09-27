using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace BlazorWebassemblyWebAPI.Services.Interfaces
{
    public interface IJwtParser
    {
        public IEnumerable<Claim> ParseClaimsFromJwt(string jwt);

        private byte[] ParseBase64WithoutPadding(string base64)
        {
           throw new NotImplementedException(); 
        } 
    }
}