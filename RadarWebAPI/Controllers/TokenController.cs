using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System;
using RadarWebAPI.Settings;
using Microsoft.Extensions.Options;
using RadarWebAPI.DB;
using Microsoft.EntityFrameworkCore;
using RadarWebAPI.Models;

namespace RadarWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("MyAllowSubdomainPolicy")]
    public class TokenController : ControllerBase
    {
        [HttpPost("/GetToken")]
        public IActionResult Token(string email, string password)
        {
            var identity = GetIdentity(email, GetHash.getHashSha256(password));
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid email or password." });
            }

            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name,
                id = identity.Claims.Last(x => x.Issuer!=null).Value

            };

            return Ok(response);
        }
        public DbContextOptions<AplicationContext> options = DBHelper.Option();
        private ClaimsIdentity GetIdentity(string email, string password)
        {
            using (var db = new AplicationContext(options))
            {
                var person = db.Users.FirstOrDefault(x => x.Email == email && x.Password == password);
                if (person != null)
                {
                    var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, person.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Password),
                new Claim(ClaimsIdentity.DefaultIssuer, person.Id.ToString())
            };
                    ClaimsIdentity claimsIdentity =
                    new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);
                    return claimsIdentity;
                }

                // если пользователя не найдено
                return null;
            }
        }
    }
}
