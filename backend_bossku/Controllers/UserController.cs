using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MySqlX.XDevAPI.Common;
using backend_bossku._1_Core.Entities;
using backend_bossku._1_Core.Entities.SubEntities;
using backend_bossku._2_Service.Service;
using backend_bossku._2_Service.Service.Interface;

namespace backend_bossku.Controllers
{
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IConfiguration _config;
        public UserController(IUserService userService, IConfiguration config)
        {
            this.userService = userService;
            _config = config;
        }
        private string GenerateJSONWebToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [Route("Api/[controller]/Register")]
        [HttpPost]
        public async Task<ActionResult> Register([FromBody] User user)
        {
            var result = await userService.Create(user);
            return Ok(result);
        }
        [Route("Api/[controller]/Login")]
        [HttpPost]
        //[Authorize]
        public async Task<UserJWT> Login([FromBody] loginUser user)
        {
            IActionResult response = Unauthorized();
            var result = await userService.Login(user);
            UserJWT final = new UserJWT();
            if (result== "You've been logged in")
            {
                var tokenString = GenerateJSONWebToken();
                response = Ok(new { token = tokenString });
                User getUser = await userService.GetUser(user.Email);
                final.IdUser = getUser.IdUser;
                final.Name = getUser.Name;
                final.Email = user.Email;
                final.Token = tokenString;
            }
            return final;
        }
        [Route("Api/[controller]/ResetPassword")]
        [HttpPost]
        public async Task<List<loginUser>> GetByCategory([FromBody] ResetEmail email)
        {
            List<loginUser> result = await userService.GetPass(email.Email);
            return result;
        }
        [Route("Api/[controller]/ResetPassword/NewPass")]
        [HttpPut]
        public async Task<IActionResult> ResetPassword([FromBody] loginUser user)
        {
            await userService.Update(user);
            return Ok("Password has been reset!");
        }

        /*        [HttpGet]
                public async Task<List<User>> Get()
                {
                    List<User> result = await userService.Get();
                    return result;
                }*/
        /*        [HttpDelete]
                public async Task<IActionResult> Delete([FromBody] int id)
                {
                    var result = await userService.Delete(id);
                    return Ok(result);
                }*/
    }
}
