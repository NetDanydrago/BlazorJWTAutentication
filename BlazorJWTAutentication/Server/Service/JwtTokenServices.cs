using BlazorJWTAutentication.Server.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlazorJWTAutentication.Server.Service
{
    public class JwtTokenServices : IJwtTokenService
    {
        private readonly IConfiguration Configuration;

        public JwtTokenServices(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public string BuildToken(string email)
        {
            var Claims = new[] {

                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]));

            var Credential = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

            var Token = new JwtSecurityToken(Configuration["Jwt:Issuer"],
                Configuration["Jwt:Audience"],
                Claims,
                expires: DateTime.Now.AddMinutes(double.Parse(Configuration["Jwt:ExpireTime"])),
                signingCredentials: Credential);

            return new JwtSecurityTokenHandler().WriteToken(Token);

        }
    }
}
