using EventCatalogAPI.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace EventCatalogAPI.Data
{
    public class EventContext: DbContext
    {
        
        public EventContext(DbContextOptions options): base(options)
        {

        }
   //EventTypes,EventItems,EventLocations  are the internal names that I use to talk to the tables     
        public DbSet<EventType> EventTypes{ get; set; }
        public  DbSet<EventItem>  EventItems{ get; set; }
        public DbSet<EventLocation> EventLocations{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           

            modelBuilder.Entity<EventType>(e =>
            {
                e.ToTable("EventTypes");
                e.Property(t => t.ID)
                .IsRequired()
                .UseHiLo("Event_Types_hilo");

                e.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);
            });

            modelBuilder.Entity<EventLocation>(e =>
            {
                //Below we are asking Entity frame work to convert into table name EventLocations
                e.ToTable("EventLocations");
                e.Property(l => l.EventLocationID)
                .IsRequired()
                .UseHiLo("Event_Location_hilo");
                //Event_Location_hilo is the internal Sql Server name
                //HoLo means DB will generate Id within range

               // e.HasKey(e => e.EventLocationID);

                e.Property(l => l.Address)
                .IsRequired()
                .HasMaxLength(200);

                e.Property(l => l.EventDate)
                .IsRequired();

            });

            modelBuilder.Entity<EventItem>(e =>
            {
                e.ToTable("EventItems");
                e.Property(a => a.ID)
                .IsRequired()
                .UseHiLo("Event_Items_hilo");

                e.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(200);

                e.Property(a => a.Price)
                .IsRequired();

                 e.HasOne(a => a.EventType)
                .WithMany()
                .HasForeignKey(a => a.EventTypeId);

                e.HasOne(a => a.EventLocation)
                .WithMany()
                .HasForeignKey(a => a.EventLocationId);

            });
        }
    }
}
