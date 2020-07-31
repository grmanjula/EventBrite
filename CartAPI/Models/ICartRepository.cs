using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CartAPI.Models
{
    public interface ICartRepository
    {
        Task<Cart> GetCartAsync(string cartId);
        Task<Cart> UpdateCart(Cart basket);
        Task<bool> DeleteCart(string id);
        IEnumerable<string> GetUsers();
    }
}
