using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CartAPI.Models
{
    public class Cart
    {
        public string BuyerId { get; set; }
        public List<CartItem> Items { get; set; }

        public Cart()
        {

        }

        public Cart(string cartid)
        {
            BuyerId = cartid;
            Items = new List<CartItem>();
        }
    }
}
