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
        public DbSet<Event> Events { get; set; }
        public DbSet<EventCategory> Categories { get; set; }

    }
}
