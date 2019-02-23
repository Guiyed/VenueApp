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
                new Event { ID = 2, Name = "Test Event 2", Description = "Description of Test Event 2.", Date = DateTime.Today, Price = 10.50, CategoryID = 1, Location = "Launchcode. Miami, Florida", Created = DateTime.Now, Protected = false },
                new Event { ID = 3, Name = "Music Event", Description = "Description of Music Event", Date = new DateTime(2019, 03, 01, 18, 10, 0, new CultureInfo("en-US", false).Calendar), Price = 10.99, CategoryID = 2, Location = "Miami, Florida", Created = DateTime.Now, Protected = false },
                new Event { ID = 4, Name = "Art Event", Description = "Description of Art Event", Date = new DateTime(2019, 03, 02, 17, 00, 0, new CultureInfo("en-US", false).Calendar), Price = 20.49, CategoryID = 3, Location = "Ft. Lauderdale, Florida", Created = DateTime.Now, Protected = false },
                new Event { ID = 5, Name = "Business Event", Description = "Description of Business Event", Date = new DateTime(2019, 03, 05, 8, 15, 0, new CultureInfo("en-US", false).Calendar), Price = 17.00, CategoryID = 4, Location = "Miramar, Florida", Created = DateTime.Now, Protected = false },
                new Event { ID = 6, Name = "Party Event", Description = "Description of Party Event", Date = new DateTime(2019, 03, 12, 12, 45, 0, new CultureInfo("en-US", false).Calendar), Price = 90.00, CategoryID = 5, Location = "Coral Gables, Florida", Created = DateTime.Now, Protected = false },
                new Event { ID = 7, Name = "Classes Event", Description = "Description of Class Event", Date = new DateTime(2019, 03, 28, 10, 25, 10, new CultureInfo("en-US", false).Calendar), Price = 35, CategoryID = 6, Location = "Kendall, Florida", Created = DateTime.Now, Protected = false },
                new Event { ID = 8, Name = "Sport Event", Description = "Description of Sport Event", Date = new DateTime(2019, 04, 15, 21, 27, 10, new CultureInfo("en-US", false).Calendar), Price = 49.98, CategoryID = 7, Location = "Weston. Miami, Florida", Created = DateTime.Now, Protected = false },
                new Event { ID = 9, Name = "Food & Drink Event", Description = "Description of Food & Drink Event", Date = new DateTime(2019, 08, 01, 18, 35, 30, new CultureInfo("en-US", false).Calendar), Price = 12.00, CategoryID = 8, Location = "Sawgrass, Florida", Created = DateTime.Now, Protected = false }
                );

        }
    }
}
