using Microsoft.AspNetCore.Mvc;
using backend_bossku._1_Core.Entities;
using backend_bossku._2_Service.Service.Interface;
using backend_bossku._3_Data.Data.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend_bossku._1_Core.Entities.SubEntities;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;

namespace backend_bossku._2_Service.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly MySqlConnection _db;

        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            this.userRepository = userRepository;
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            _db = new MySqlConnection(connectionString);
        }
        public async Task<bool> Create([FromBody] User user)
        {
            var command = userRepository.CreateUser();
            string pwdHashed = BCrypt.Net.BCrypt.HashPassword(user.Password);
            using (MySqlCommand cmd = new MySqlCommand(command, _db))
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
            using (MySqlCommand cmd = new MySqlCommand(command, _db))
            {
                await _db.OpenAsync();
                cmd.Parameters.AddWithValue("@Email", email);
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        result.Add(new loginUser
                        {
                            Email = reader["Email"].ToString(),
                            Password = reader["Password"].ToString(),
                        });
                    }
                }
                await _db.CloseAsync();
            }
            return result;
        }

        public async Task<User> GetUser(string email)
        {
            string command = userRepository.GetUser();
            var result = new User();
            using (MySqlCommand cmd = new MySqlCommand(command, _db))
            {
                await _db.OpenAsync();
                cmd.Parameters.AddWithValue("@Email", email);
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        result.Name = reader["name"].ToString();
                        result.IdUser = Convert.ToInt32(reader["idUser"]);
                    }
                }
                await _db.CloseAsync();
                return result;
            }
        }

        public async Task<string> Login([FromBody] loginUser user)
        {
            string command = userRepository.LoginUser();
            string result = "";
            using (MySqlCommand cmd = new MySqlCommand(command, _db))
            {
                await _db.OpenAsync();
                cmd.Parameters.AddWithValue("@Email", user.Email);
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        string resEmail = reader["Email"].ToString();
                        string resPassword = reader["Password"].ToString();
                        if (user.Email == resEmail && BCrypt.Net.BCrypt.Verify(user.Password, resPassword))
                        {
                            result = "You've been logged in";
                        }
                        else
                        {
                            result = "Account Invalid!";
                        }
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
            using (MySqlCommand cmd = new MySqlCommand(command, _db))
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
