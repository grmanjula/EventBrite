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
                //To save the changes in the DB
                //Records are not committed in DB. Very important step
                eventContext.SaveChanges();

            }
            if (!eventContext.EventItems.Any())
            {

            }

        }

        private static IEnumerable<EventLocation> GetEventLocations()
        {
            return new List<EventLocation>
           {
                new EventLocation
                {
                    Address = "Redmond,WA",
                    Mode = "Online"
                },
                new EventLocation
                {
                    Address = "Redmond,WA",
                    Mode = "InPerson"
                },
                new EventLocation
                {
                    Address = "Bellevue,WA",
                    Mode = "InPerson"
                },

                new EventLocation
                {
                    Address = "Bellevue,WA",
                    Mode = "Online"
                },

                new EventLocation
                {
                    Address = "Irvine,CA",
                    Mode = "InPerson"
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


            new EventItem{EventLocationId = 3, EventTypeId = 2, Name = "Jazz Music", Price = 45, Description ="Musical Event By Matt H Redmond", ImageUrl = "http://externaleventbaseurltobeplaced/api/pic/1"},
            new EventItem{EventLocationId = 2, EventTypeId = 3, Name = "Computer Science", Price = 55, Description ="Educational Event By Praveen Redmond", ImageUrl = "http://externaleventbaseurltobeplaced/api/pic/2"}


            };

        }




    }
}









