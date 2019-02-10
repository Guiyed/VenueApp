using Microsoft.EntityFrameworkCore;
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



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();

            modelBuilder.Entity<User>().HasData(
                new User { ID = 1, Username = "admin", Password = "password", MembershipID = 1, TypeID = 1 ,Protected = true }
                );

            modelBuilder.Entity<Membership>().HasData(
                new Membership { ID = 1, Name = "none", Protected = true }
                );

            modelBuilder.Entity<UserType>().HasData(
                new UserType { ID = 1, Name = "admin", Protected = true },
                new UserType { ID = 2, Name = "user", Protected = true }
                );

            /*
            modelBuilder.Entity<UserEvent>()
                .HasKey(c => new { c.UserID, c.EventID });
                */
        }




    }
}
