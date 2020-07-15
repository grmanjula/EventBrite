using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Models
{
    public class EventItem
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public int EventTypeId { get; set; }
        public int EventLocationId { get; set; }

       
        public string EventType { get; set; }
      
        public string EventLocation { get; set; }
    }
}

