using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using NLayer.Data.Models;
using NLayer.Service;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace RegistrationGuide_V2._0.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly GetToken _getToken;


        public AuthController(GetToken getToken)
        {
            _getToken = getToken;
        }
        [HttpPost]       
        public async Task<IActionResult> GetToken([FromBody] GetTokenMultipleParameters parameters)
        {
          var token = await _getToken.CreateToken(parameters.userame, parameters.password);
            return Ok(token);
        }
    }
    }
    
        
