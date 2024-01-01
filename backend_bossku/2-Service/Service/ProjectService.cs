using backend_bossku._1_Core.Entities;
using backend_bossku._1_Core.Entities.SubEntities;
using backend_bossku._2_Service.Service.Interface;
using backend_bossku._3_Data.Data;
using backend_bossku._3_Data.Data.Interface;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace backend_bossku._2_Service.Service
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository cartRepository;
        private readonly SqlConnection _db;

        public ProjectService(IProjectRepository cartRepository)
        {
            this.cartRepository = cartRepository;
            _db = new SqlConnection("data source=.; database=SoupDatabase; integrated security=SSPI");
        }
        public async Task<bool> CreateCart(Project cart) 
        {
            try
            {
                var command = cartRepository.CreateCartDB();
                using (SqlCommand cmd = new SqlCommand(command, _db))
                {
                    for (int i = 0; i < cart.CourseId.Length; i++)
                    {
                        await _db.OpenAsync();
                        cmd.Parameters.AddWithValue("@UserId", cart.UserId);
                        cmd.Parameters.AddWithValue("@CourseId", cart.CourseId[i]);
                        cmd.Parameters.AddWithValue("@Schedule", cart.Schedule);
                        await cmd.ExecuteNonQueryAsync();
                        cmd.Parameters.Clear();
                        await _db.CloseAsync();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
/*        public async Task<CartContent> GetCartByUserId(int userId)
        {


        }*/
    }
}
