using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventCatalogAPI.Domain
{
    public class EventLocation
    {
        public int EventLocationID { get; set; }
        public DateTime EventDate { get; set; }
        public string Address { get; set; }
        public string Mode { get; set; }
    }
}
