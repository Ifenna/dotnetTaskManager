using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using taskManager_API.Interface.IService;
using taskManager_API.models;

namespace taskManager_API.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _iconfig;
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration config)
        {
            _iconfig = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_iconfig["JWT:SigningKey"]));      
        }
        public string CreateToken(AppUser appUser)
        {
            var claims = new List<Claim>{
                new Claim(JwtRegisteredClaimNames.Email, appUser.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, appUser.UserName)
            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = creds,
                Issuer = _iconfig["JWT:Issuer"],
                Audience = _iconfig["JWT:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}