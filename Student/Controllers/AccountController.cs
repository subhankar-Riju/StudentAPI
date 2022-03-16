using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Student.Model;
using Student.Repository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Student.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _account;
        private readonly IConfiguration _config;

        public AccountController(IAccountRepository account,IConfiguration config)
        {
            _account = account;
            _config = config;
        }

        [HttpPost("/signup")]

        public async Task<IActionResult> SignUpAsync([FromBody] SignUpModel signUp)
        {
             await _account.SignUpAsyn(signUp);
            
            

            return Ok();
        }

        [HttpPost("/login")]
        public async Task<IActionResult> Loginasync([FromBody] LoginModel loginModel)
        {
            var record = await _account.Loginasync(loginModel);
            
            if (record.ToList().Count==1)
            {
                var token = GenerateToke(loginModel.email);
                return Ok(new 
                { 
                    Data=record,
                    Token=token

                });
            }
            else
            {
                return Unauthorized();
            }

        }

        private string GenerateToke(string name)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["jwt:key"]));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email,name),
                new Claim("CompanyName","tcs")
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires:DateTime.Now.AddDays(1),
                signingCredentials:credential
                );

            return tokenHandler.WriteToken(token);
        }

    }
}
