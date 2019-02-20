using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using VenueApp.Models;

namespace VenueApp.Data
{
    public class VenueAppDbContext : DbContext
    {

        public VenueAppDbContext(DbContextOptions<VenueAppDbContext> options)
            : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<UserType> Types { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventCategory> Categories { get; set; }
        public DbSet<Booking> Bookings { get; set; }


        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();

            modelBuilder.Entity<Booking>()
                .HasKey(c => new { c.UserID, c.EventID });

            modelBuilder.Entity<EventCategory>().HasData(
                new EventCategory { ID = 1, Name = "none", Protected = true },
                new EventCategory { ID = 2, Name = "Music", Protected = true },
                new EventCategory { ID = 3, Name = "Arts", Protected = true },
                new EventCategory { ID = 4, Name = "Business", Protected = true },
                new EventCategory { ID = 5, Name = "Parties", Protected = true },
                new EventCategory { ID = 6, Name = "Classes", Protected = true },
                new EventCategory { ID = 7, Name = "Sports", Protected = true },
                new EventCategory { ID = 8, Name = "Food & Drikns", Protected = true }
                );

            modelBuilder.Entity<Membership>().HasData(
                new Membership { ID = 1, Name = "none", Protected = true },
                new Membership { ID = 2, Name = "Bronze", Protected = true },
                new Membership { ID = 3, Name = "Silver", Protected = true },
                new Membership { ID = 4, Name = "Gold", Protected = true }
                );

            modelBuilder.Entity<UserType>().HasData(
                new UserType { ID = 1, Name = "admin", Protected = true },
                new UserType { ID = 2, Name = "user", Protected = true }
                );

            modelBuilder.Entity<User>().HasData(
                new User { ID = 1, Username = "admin", Password = "password", Created = DateTime.Now, MembershipID = 1, TypeID = 1, Protected = true },
                new User { ID = 2, Username = "user", Password = "password", Created = DateTime.Now, MembershipID = 1, TypeID = 2, Protected = true }
                );

            modelBuilder.Entity<Event>().HasData(
                new Event { ID = 1, Name = "Test Event 1", Description = "Description of Test Event 1.", Date = new DateTime(2019, 07, 28, 18, 35, 5, new CultureInfo("en-US", false).Calendar), Price = 9.99, CategoryID = 1, Location = "Launchcode. Miami, Florida", Created = DateTime.Now, Protected = true },
                new Event { ID = 2, Name = "Test Event 2", Description = "Description of Test Event 2.", Date = DateTime.Today, Price= 10.50, CategoryID = 1, Location="Launchcode. Miami, Florida", Created = DateTime.Now, Protected = false }
                );


        }
    }
}
