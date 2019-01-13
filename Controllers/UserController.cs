using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Peoplee.API.BusinessLayer;
using Peoplee.API.DtoModels;

namespace Peoplee.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
         private readonly UserManager userManager;
         private readonly IConfiguration _config;
  
         public UserController(UserManager manager, IConfiguration config)
         {
             userManager = manager;      
             _config = config;
         }


        [HttpPost("register")]
         public ActionResult Register(UserViewModel userModel){

            userManager.Register(userModel);

               //  Console.WriteLine();

             return StatusCode(201);
         }

          [HttpPost("login")]
         public ActionResult Login(UserViewModel userModel){

            var user = userManager.Login(userModel);

            if(user == null) Unauthorized();

            var claims = new []{
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };
            
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(10),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);


             return Ok(new {
                 token = tokenHandler.WriteToken(token)
             });
         }
    }
}