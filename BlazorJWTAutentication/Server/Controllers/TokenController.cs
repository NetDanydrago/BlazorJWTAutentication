using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorJWTAutentication.Server.Interface;
using BlazorJWTAutentication.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorJWTAutentication.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IJwtTokenService TokenServices;

        public TokenController(IJwtTokenService tokenService)
        {
            TokenServices = tokenService;
        }

        [HttpPost]
        public IActionResult GeneraToken(ModelToken modelToken)
        {
            var Token = TokenServices.BuildToken(modelToken.Email);
            return Ok(new {Token});
        }
    }
}