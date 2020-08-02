using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Models.CartModels
{
    public class Cart
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        public string BuyerId { get; set; }

        //Method to calculate the total items in the cart
        public decimal Total()
        {
            //For every item in the cart I am taking the unitprice multiplying with quantity and rounding it
            return Math.Round(Items.Sum(x => x.UnitPrice * x.Quantity), 2);
        }

    }
}
