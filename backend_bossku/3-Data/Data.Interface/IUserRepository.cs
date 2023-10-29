using backend_bossku._1_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_bossku._3_Data.Data.Interface
{
    public interface IUserRepository
    {
        public string GetUser();
        public string CreateUser();
        public string GetPassword();
        public string LoginUser();
        public string UpdateUser();
        /*        public Task<List<User>> GetUser();*/
        /*        public Task<bool> DeleteUser(int id);*/
    }
}
