using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using backend_bossku._1_Core.Entities;

namespace backend_bossku._2_Service.Service.Interface
{
    public interface ICourseService
    {
        public Task<bool> Create(Course course);
        public Task<List<Course>> Get();
        public Task<List<Course>> GetByCategory(string category);
        public Task<List<Course>> GetById(int id);
        /*        public Task<Course> Update([FromBody] Course course);
                public Task<bool> Delete([FromBody] int id);*/
    }
}
