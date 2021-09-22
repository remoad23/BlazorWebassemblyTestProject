using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace BlazorWASMProject.Services.Interfaces
{
    public interface IJwtParser
    {
        public IEnumerable<Claim> ParseClaimsFromJwt(string jwt);
        private byte[] ParseBase64WithoutPadding(string base64) => throw NotImplementedException((e) => e);
    }
}