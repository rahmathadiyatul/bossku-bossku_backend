using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_bossku._1_Core.Entities;
using backend_bossku._2_Service.Service;
using backend_bossku._2_Service.Service.Interface;
using backend_bossku._3_Data.Data.Interface;

namespace backend_bossku._3_Data.Data
{
    public class ServiceRepository : IServiceRepository
    {
        public string CreateCourse()
        {
            var result = "INSERT INTO dbo.course " +
                "(name, category, price, description, img) values " +
                "(@Name, @Category, @Price, @Desc, @Img)";
            return result;
        }

        public string GetCourse()
        {
            var result = "SELECT * FROM course";
            return result;
        }

        public string GetCourseByCategory()
        {
            var result = "SELECT * FROM course " +
                "WHERE category = @Category";
            return result;
        }

        public string GetCourseById()
        {
            var result = "SELECT * FROM course " +
                "WHERE idCourse = @Id";
            return result;
        }

        /*        public async Task<Course> UpdateCourse(Course course)
                {
                    await _dBService.ModifyDataCourse("UPDATE dbo.course" +
                        " SET name=@Name, category=@Category, price=@Price, description=@Desc, img=@Img " +
                        "WHERE idCourse=@IdCourse;", course);
                    return course;
                }*/

        /*        public async Task<bool> DeleteCourse(int id)
                {
                    await _dBService.DeleteInvoiceCourse("DELETE FROM dbo.course_invoice_relation ci " +
                        "WHERE ci.courseId = @id;", new { id });
                    await _dBService.DeleteDataCourse("DELETE FROM dbo.course c " +
                        "WHERE c.idCourse = @id;", new { id });
                    return true;
                }*/
    }
}
