using EventCatalogAPI.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace EventCatalogAPI.Data
{

    public static class EventSeed

    {
        //Anywhere you pass EventContext imagine you are passing Data base Context
        public static void Seed(EventContext eventContext)
        {
            //The below command will take all your migrations and run then to apply against the local database to create the tables.
            //If the tables are already there it will not make any changes
            //DB setup
            eventContext.Database.Migrate();

            //If I don't have any data then add data to Event Location table
            if (!eventContext.EventLocations.Any())
            {
                eventContext.EventLocations.AddRange(GetEventLocations());
                //To save the changes in the DB
                //Records are not committed in DB. Very important step
                eventContext.SaveChanges();
            }
            if (!eventContext.EventTypes.Any())
            {
                eventContext.EventTypes.AddRange(GetEventTypes());
               
                eventContext.SaveChanges();

            }
            if (!eventContext.EventItems.Any())
            {
                eventContext.EventItems.AddRange(GetEventItems());
              
                eventContext.SaveChanges();

            }

        }

        private static IEnumerable<EventLocation> GetEventLocations()
        {
            return new List<EventLocation>
           {
                new EventLocation
                {
                    Address = "Redmond,WA",
                    Mode = "Online",
                    EventDate = Convert.ToDateTime(12/12/2020)
                },

                new EventLocation
                {
                    Address = "Redmond,WA",
                    Mode = "InPerson",
                    EventDate = Convert.ToDateTime(12/12/2021),
                },
                new EventLocation
                {
                    Address = "Bellevue,WA",
                    Mode = "InPerson",
                    EventDate = Convert.ToDateTime(12/12/2022),
                },

                new EventLocation
                {
                    Address = "Bellevue,WA",
                    Mode = "Online",
                    EventDate = Convert.ToDateTime(12/12/2023),
                },

                new EventLocation
                {
                    Address = "Irvine,CA",
                    Mode = "InPerson",
                    EventDate = Convert.ToDateTime(12/12/2024),
                },
                new EventLocation
                {
                    Address = "Irvine,CA",
                    Mode = "Online"
                },


           };
        }

        private static IEnumerable<EventType> GetEventTypes()
        {
            return new List<EventType>
            {
                new EventType
                {
                    Name = "Music"

                },
                new EventType
                {
                    Name = "Education"

                },
                new EventType
                {
                    Name = "Sports"

                },

                new EventType
                {
                    Name = "Spiritual"

                },
                new EventType
                {
                    Name = "Other"

                },
            };

        }

        private static IEnumerable<EventItem> GetEventItems()
        {
            return new List<EventItem>()
            {

            new EventItem{EventLocationId = 1, EventTypeId = 11, Name = "Jazz Music", Price = 15, Description ="Musical Event By Matt H Redmond", ImageUrl = "http://externaleventbaseurltobeplaced/api/image/1"},
            new EventItem{EventLocationId = 2, EventTypeId = 12, Name = "Computer Science", Price = 25, Description ="Educational Event By Praveen Redmond", ImageUrl = "http://externaleventbaseurltobeplaced/api/image/2"},
            new EventItem{EventLocationId = 3, EventTypeId = 13, Name = "Sports", Price = 35, Description ="Sports Event By SeaHawks", ImageUrl = "http://externaleventbaseurltobeplaced/api/image/3"},
            new EventItem{EventLocationId = 4, EventTypeId = 14, Name = "Spiritual", Price = 45, Description ="Aradhana", ImageUrl = "http://externaleventbaseurltobeplaced/api/image/4"},
            new EventItem{EventLocationId = 5, EventTypeId = 15, Name = "Jazz Music", Price = 55, Description ="Musical Event By Matt H Redmond", ImageUrl = "http://externaleventbaseurltobeplaced/api/image/5"},
            
            };

        }




    }
}









