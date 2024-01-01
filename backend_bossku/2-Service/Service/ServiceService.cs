using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using backend_bossku._1_Core.Entities;
using backend_bossku._2_Service.Service.Interface;
using backend_bossku._3_Data.Data.Interface;

namespace backend_bossku._2_Service.Service
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository courseRepository;
        private readonly SqlConnection _db;

        public ServiceService(IServiceRepository courseRepository)
        {
            this.courseRepository = courseRepository;
            _db = new SqlConnection("data source=.; database=SoupDatabase; integrated security=SSPI");
        }

        public async Task<bool> Create(_1_Core.Entities.Service course)
        {
            var command = courseRepository.CreateCourse();
            using (SqlCommand cmd = new SqlCommand(command, _db))
            {
                cmd.Parameters.AddWithValue("@IdCourse", course.IdCourse);
                cmd.Parameters.AddWithValue("@Name", course.Name);
                cmd.Parameters.AddWithValue("@Category", course.Category);
                cmd.Parameters.AddWithValue("@Price", course.Price);
                cmd.Parameters.AddWithValue("@Desc", course.Desc);
                cmd.Parameters.AddWithValue("@Img", course.Img);
                await _db.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
                await _db.CloseAsync();
            }
            return true;
        }

        public async Task<List<_1_Core.Entities.Service>> Get()
        {
            string command = courseRepository.GetCourse();
            var result = new List<_1_Core.Entities.Service>();
            using (SqlCommand cmd = new SqlCommand(command, _db))
            {
                await _db.OpenAsync();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                while (reader.Read())
                {
                    result.Add(new _1_Core.Entities.Service
                    {
                        IdCourse = Convert.ToInt32(reader["IdCourse"]),
                        Name = reader["Name"].ToString(),
                        Category = reader["Category"].ToString(),
                        Price = Convert.ToDecimal(reader["Price"]),
                        Desc = reader["description"].ToString(),
                        Img = reader["img"].ToString()
                    });
                }
                await _db.CloseAsync();
            }
            return result;
        }

        public async Task<List<_1_Core.Entities.Service>> GetByCategory(string category)
        {
            string command = courseRepository.GetCourseByCategory();
            var result = new List<_1_Core.Entities.Service>();
            using (SqlCommand cmd = new SqlCommand(command, _db))
            {
                await _db.OpenAsync();
                cmd.Parameters.AddWithValue("@Category", category.ToLower());
                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                while (reader.Read())
                {
                    result.Add(new _1_Core.Entities.Service
                    {
                        IdCourse = Convert.ToInt32(reader["IdCourse"]),
                        Name = reader["Name"].ToString(),
                        Category = reader["Category"].ToString(),
                        Price = Convert.ToDecimal(reader["Price"]),
                        Desc = reader["description"].ToString(),
                        Img = reader["img"].ToString()
                    });
                }
                await _db.CloseAsync();
            }
            return result;
        }

        public async Task<List<_1_Core.Entities.Service>> GetById(int id)
        {
            string command = courseRepository.GetCourseById();
            var result = new List<_1_Core.Entities.Service>();
            using (SqlCommand cmd = new SqlCommand(command, _db))
            {
                await _db.OpenAsync();
                cmd.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                while (reader.Read())
                {
                    result.Add(new _1_Core.Entities.Service
                    {
                        IdCourse = Convert.ToInt32(reader["IdCourse"]),
                        Name = reader["Name"].ToString(),
                        Category = reader["Category"].ToString(),
                        Price = Convert.ToDecimal(reader["Price"]),
                        Desc = reader["description"].ToString(),
                        Img = reader["img"].ToString()
                    });
                }
                await _db.CloseAsync();
            }
            return result;
        }

        /*        public async Task<Course> Update([FromBody] Course course)
                {
                    var result = await courseRepository.UpdateCourse(course);
                    return result;
                }

                public async Task<bool> Delete([FromBody] int id)
                {
                    var result = await courseRepository.DeleteCourse(id);
                    return result;
                }*/
    }
}
