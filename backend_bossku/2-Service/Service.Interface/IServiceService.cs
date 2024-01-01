using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using backend_bossku._1_Core.Entities;

namespace backend_bossku._2_Service.Service.Interface
{
    public interface IServiceService
    {
        public Task<bool> Create(_1_Core.Entities.Service course);
        public Task<List<_1_Core.Entities.Service>> Get();
        public Task<List<_1_Core.Entities.Service>> GetByCategory(string category);
        public Task<List<_1_Core.Entities.Service>> GetById(int id);
        /*        public Task<Course> Update([FromBody] Course course);
                public Task<bool> Delete([FromBody] int id);*/
    }
}
