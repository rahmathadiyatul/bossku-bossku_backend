using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_bossku._1_Core.Entities;

namespace backend_bossku._3_Data.Data.Interface
{
    public interface ICourseRepository
    {
        public string CreateCourse();
        public string GetCourse();
        public string GetCourseByCategory();
        public string GetCourseById();
        /*        public Task<Course> UpdateCourse(Course course);
                public Task<bool> DeleteCourse(int id);*/

    }
}
