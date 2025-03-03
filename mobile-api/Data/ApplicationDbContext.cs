using Microsoft.EntityFrameworkCore;
using mobile_api.Models;

namespace mobile_api.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> configuration) : base(configuration)
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<ImageTour> ImageTours { get; set; }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<Book> Bookings { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

    }
}
