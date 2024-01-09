using backend_bossku._1_Core.Entities;
using backend_bossku._1_Core.Entities.SubEntities;
using backend_bossku._2_Service.Service.Interface;
using backend_bossku._3_Data.Data;
using backend_bossku._3_Data.Data.Interface;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace backend_bossku._2_Service.Service
{
    public class InvestorService : IInvestorService
    {
        private readonly IInvestorRepository investorRepository;
        private readonly MySqlConnection _db;

        public InvestorService(IInvestorRepository investorRepository, IConfiguration configuration)
        {
            this.investorRepository = investorRepository;
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            _db = new MySqlConnection(connectionString);
        }
        public async Task<bool> Signup(Investor investor) 
        {
            try
            {
                var command = investorRepository.SignUpInvestor();
                using (MySqlCommand cmd = new MySqlCommand(command, _db))
                {
                    cmd.Parameters.AddWithValue("@FullName", investor.FullName);
                    cmd.Parameters.AddWithValue("@Email", investor.Email);
                    cmd.Parameters.AddWithValue("@CompanyName", investor.CompanyName);
                    cmd.Parameters.AddWithValue("@Phone", investor.Phone);
                    cmd.Parameters.AddWithValue("@CompanyCat", investor.CompanyCat);
                    cmd.Parameters.AddWithValue("@CompanyLoc", investor.CompanyLoc);
                    await _db.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    await _db.CloseAsync();
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
