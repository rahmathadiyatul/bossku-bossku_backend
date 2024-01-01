using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using backend_bossku._1_Core.Entities;
using backend_bossku._2_Service.Service;
using backend_bossku._2_Service.Service.Interface;

namespace backend_bossku.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class ServiceController : Controller
    {
        private readonly IServiceService courseService;
        public ServiceController(IServiceService courseService)
        {
            this.courseService = courseService;
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Service course)
        {
            var result = await courseService.Create(course);
            return Ok(result);
        }
        [HttpGet]
        public async Task<List<Service>> Get()
        {
            List<Service> result = await courseService.Get();
            return result;
        }

        [HttpGet("getbycategory/{category}")]
        public async Task<List<Service>> GetByCategory(string category)
        {
            List<Service> result = await courseService.GetByCategory(category);
            return result;
        }

        [HttpGet("getbyid/{id}:int")]
        public async Task<List<Service>> GetById(int id)
        {
            List<Service> result = await courseService.GetById(id);
            return result;
        }
        /*        [HttpPut]
                public async Task<IActionResult> Update([FromBody] Course course)
                {
                    var result = await courseService.Update(course);
                    return Ok(result);
                }*/
        /*        [HttpDelete]
                public async Task<IActionResult> Delete([FromBody] int id)
                {
                    var result = await courseService.Delete(id);
                    return Ok(result);
                }*/
    }
}
