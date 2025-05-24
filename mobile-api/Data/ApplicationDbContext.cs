using Microsoft.EntityFrameworkCore;
using mobile_api.Models;

namespace mobile_api.Data
{
    public class ApplicationDbContext : DbContext
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
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tour>()
                .HasMany(t => t.Tags)
                .WithMany(t => t.Tours)
                .UsingEntity(j => j.ToTable("TourTags"));

            modelBuilder.Entity<Tour>()
                .HasMany(t => t.Categories)
                .WithMany(c => c.Tours)
                .UsingEntity(j => j.ToTable("TourCategories"));
            modelBuilder.Entity<Tour>()
                .HasMany(t => t.Services)
                .WithMany(s => s.Tours)
                .UsingEntity(j => j.ToTable("TourServices"));

        }
    }
}