using backend_bossku._1_Core.Entities;
using backend_bossku._1_Core.Entities.SubEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend_bossku._2_Service.Service.Interface
{
    public interface IProjectService
    {
        public Task<bool> CreateCart(Project cart);
/*        public Task<CartContent> GetCartByUserId(int userId);
*/    }
}
