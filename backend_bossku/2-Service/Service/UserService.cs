using Microsoft.AspNetCore.Mvc;
using backend_bossku._1_Core.Entities;
using backend_bossku._2_Service.Service.Interface;
using backend_bossku._3_Data.Data;
using backend_bossku._3_Data.Data.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using backend_bossku._1_Core.Entities.SubEntities;

namespace backend_bossku._2_Service.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly SqlConnection _db;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
            _db = new SqlConnection("data source=.; database=SoupDatabase; integrated security=SSPI");
        }
        public async Task<bool> Create([FromBody] User user)
        {
            var command = userRepository.CreateUser();
            string pwdHashed = BCrypt.Net.BCrypt.HashPassword(user.Password);
            using (SqlCommand cmd = new SqlCommand(command, _db))
            {
                cmd.Parameters.AddWithValue("@Name", user.Name);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Password", pwdHashed);
                await _db.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
                await _db.CloseAsync();
            }
            return true;
        }

        public async Task<List<loginUser>> GetPass(string email)
        {
            string command = userRepository.GetPassword();
            var result = new List<loginUser>();
            using (SqlCommand cmd = new SqlCommand(command, _db))
            {
                await _db.OpenAsync();
                cmd.Parameters.AddWithValue("@Email", email);
                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                while (reader.Read())
                {
                    result.Add(new loginUser
                    {
                        Email = reader["Email"].ToString(),
                        Password = reader["Password"].ToString(),
                    });
                }
                await _db.CloseAsync();
            }
            return result;
        }

        public async Task<User> GetUser(string email)
        {
            string command = userRepository.GetUser();
            var result = new User();
            using (SqlCommand cmd = new SqlCommand(command, _db))
            {
                await _db.OpenAsync();
                cmd.Parameters.AddWithValue("@Email", email);
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                while (reader.Read())
                {
                    result.Name = reader["name"].ToString();
                    result.IdUser = Convert.ToInt32(reader["idUser"]);

                }
                await _db.CloseAsync();
                return result;
            }
        }

        public async Task<string> Login([FromBody] loginUser user)
        {
            string command = userRepository.LoginUser();
            string result = "";
            using (SqlCommand cmd = new SqlCommand(command, _db))
            {
                await _db.OpenAsync();
                cmd.Parameters.AddWithValue("@Email", user.Email);
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                while (reader.Read())
                {
                    string resEmail = reader["Email"].ToString();
                    string resPassword = reader["Password"].ToString();
                    if (user.Email == resEmail && BCrypt.Net.BCrypt.Verify(user.Password, resPassword) == true)
                    {
                        result = "You've been logged in";
                    }
                    else
                    {
                        result = "Account Invalid!";
                    }
                }
                await _db.CloseAsync();
            }
            return result;
        }

        public async Task<bool> Update([FromBody] loginUser user)
        {
            string command = userRepository.UpdateUser();
            string pwdHashed = BCrypt.Net.BCrypt.HashPassword(user.Password);
            using (SqlCommand cmd = new SqlCommand(command, _db))
            {
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Password", pwdHashed);
                await _db.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
                await _db.CloseAsync();
            }
            return true;
        }

        /*        public async Task<List<User>> Get()
                {
                    List<User> result = await userRepository.GetUser();
                    return result;
                }*/
        /*        public async Task<bool> Delete([FromBody] int id)
                {
                    var result = await userRepository.DeleteUser(id);
                    return result;
                }*/
    }
}
