using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using backend_bossku._1_Core.Entities.SubEntities;
using backend_bossku._1_Core.Entities;
using backend_bossku._2_Service.Service.Interface;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using System;
using backend_bossku._2_Service.Service;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace backend_bossku.Controllers
{
    [ApiController]
    public class ProjectController : Controller
    {
        private readonly IProjectService cartService;
        private readonly IConfiguration _config;
        public ProjectController(IProjectService cartService, IConfiguration config)
        {
            this.cartService = cartService;
            _config = config;
        }

        [Route("Api/[controller]/Create")]
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Project cart)
        {

            var result = await cartService.CreateCart(cart);
            if (result == true)
            {
                return Ok(result);
            } else
            {
                return BadRequest(new Exception("Server Error!"));
            }
        }
/*        [Route("Api/[controller]/Get")]
        [HttpPost]
        public async Task<CartContent> Get([FromBody] int userId)
        {
            var result = await cartService.GetCartByUserId(userId);
            return result;
        }*/
    }
}
