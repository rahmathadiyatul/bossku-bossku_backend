using backend_bossku._1_Core.Entities;
using backend_bossku._2_Service.Service.Interface;
using backend_bossku._3_Data.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_bossku._3_Data.Data
{
    public class UserRepository : IUserRepository
    {
        public string CreateUser()
        {
            var result = "INSERT INTO users " +
                "(name, email, password, role) VALUES " +
                "(@Name, @Email, @Password, 'user');";
            return result;
        }
        public string GetPassword()
        {
            var result = "SELECT email, password FROM users WHERE email = @Email LIMIT 1;";
            return result;
        }

        public string GetUser()
        {
            var result = "SELECT idUser, name, email, password, role FROM users WHERE email = @Email LIMIT 1;";
            return result;
        }

        public string LoginUser()
        {
            var result = "SELECT * FROM users WHERE email = @Email;";
            return result;
        }
        public string UpdateUser()
        {
            var result = "UPDATE users SET password=@Password WHERE email=@Email;";
            return result; 
        }
        /*        public async Task<bool> DeleteUser(int id)
                {
                    await _dBService.DeleteDataUser("DELETE FROM user u " +
                        "WHERE u.idUser = @id;", new { id });
                    return true;
                }*/

        /*        public async Task<List<User>> GetUser()
                {
                    var result = await _dBService.GetDataUser<User>("SELECT * FROM user");
                    return result;
                }*/


    }
}
