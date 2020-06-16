using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventCatalogAPI.Domain
{
    public class EventItem
    {
        //Below are all the columns in the table
        public int ID { get; set; }
        public string  Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl  { get; set; }
        public int EventTypeId { get; set; }
        public int EventLocationId { get; set; }

        //The below is one way of saying that my Event item is realted to Event type table
        public EventType EventType { get; set; }
        //The below is one way of saying that my Event item is realted to Event Location table
        public EventLocation EventLocation { get; set; }
    }
}
