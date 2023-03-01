using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NLayer.Data.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service
{
    public class GetToken
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly TokenOptionsModel _tokenOptions;

        public GetToken(UserManager<AppUser> userManager,IOptions<TokenOptionsModel>  tokenOptions)
        {
            _userManager = userManager;
            _tokenOptions = tokenOptions.Value;
        }

        public async Task<string> CreateToken(string username,string password)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                return ("Kullanıcı adı veya şifre hatalı");
            }
            if (!await _userManager.CheckPasswordAsync(user, password))
            {
                return ("Kullanıcı adı veya şifre hatalı");
            }
            var userRoles = await _userManager.GetRolesAsync(user);
            var accessTokenExpiration = DateTime.UtcNow.AddMinutes(_tokenOptions.AccessTokenExpiration);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey));

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Aud, _tokenOptions.Audience[0]));

            userRoles.ToList().ForEach(x =>
            {
                claims.Add(new Claim(ClaimTypes.Role, x));
            });

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: _tokenOptions.Issuer,
                expires: accessTokenExpiration,
                notBefore: DateTime.UtcNow,
                claims: claims,
                signingCredentials: signingCredentials);

            var handler = new JwtSecurityTokenHandler();
            var token = handler.WriteToken(jwtSecurityToken);

            return token;
        }

        public string CreateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }

    }
}
