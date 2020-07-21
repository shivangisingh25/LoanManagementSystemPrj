
using LoanManagementSystemPrj.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private readonly LoanManagementContext db;
        public LoginController(IConfiguration config,LoanManagementContext _db)
        {
            _config = config;
            db = _db;
        }
        [HttpGet]
        [Route("userlogin")]
        public IActionResult userLogin(string name, string pass)
        {
            IActionResult response = Unauthorized();
            Usercredentials val = new Usercredentials
            {
                Id = 000,
                Username = name,
                Password = pass
            };
            var user = AuthenticteUser(val);
            if (user != null)
            {
                var tokenstr = GenerateJSONWebToken();
                response = Ok(new { token = tokenstr });
            }
            return response;
        }
        [HttpGet]
        [Route("adminlogin")]
        public IActionResult adminLogin(string name, string pass)
        {
            IActionResult response = Unauthorized();
            Admincredentials val = new Admincredentials
            {
                Username = name,
                Password = pass
            };
            var user = AuthenticteAdmin(val);
            if (user != null)
            {
                var tokenstr = GenerateJSONWebToken();
                response = Ok(new { token = tokenstr });
            }
            return response;
        }

        Admincredentials AuthenticteAdmin(Admincredentials login)
        {
            Admincredentials val = null;
            val = db.Admincredentials.Where(x => x.Username == login.Username && x.Password == login.Password).FirstOrDefault();
            if (val != null)
            {
                return val;
            }
            else
                return null;
        }
       Loan AuthenticteUser(Usercredentials login)
        {
            Loan valc = new Loan();
            Usercredentials val = null;
            val = db.Usercredentials.Where(x => x.Username == login.Username && x.Password == login.Password).FirstOrDefault();
            if (val != null)
            {
                int a = val.Id;
                valc = db.Loan.Where(x => x.Id == a).FirstOrDefault();
                return valc;
            }
            else
                return null;
        }

        string GenerateJSONWebToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                //new Claim(JwtRegisteredClaimNames.Sub,info.Name),
                //new Claim(JwtRegisteredClaimNames.NameId,info.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };
            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Issuer"], null,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);
            var encodetoken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodetoken;
        }
    }
}
