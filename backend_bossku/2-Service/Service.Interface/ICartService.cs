using backend_bossku._1_Core.Entities;
using backend_bossku._1_Core.Entities.SubEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend_bossku._2_Service.Service.Interface
{
    public interface ICartService
    {
        public Task<bool> CreateCart(Cart cart);
/*        public Task<CartContent> GetCartByUserId(int userId);
*/    }
}
