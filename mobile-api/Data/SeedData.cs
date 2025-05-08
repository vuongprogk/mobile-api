using mobile_api.Models;
using mobile_api.Repositories.Interfaces;
using BCrypt.Net;

namespace mobile_api.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
            var tourRepository = scope.ServiceProvider.GetRequiredService<ITourRepository>();
            var serviceRepository = scope.ServiceProvider.GetRequiredService<IServiceRepository>();
            var bookRepository = scope.ServiceProvider.GetRequiredService<IBookRepository>();

            // Ensure database is created
            context.Database.EnsureCreated();

            // Seed admin user
            await SeedAdminUser(userRepository, context);

            // Seed sample data
            var tours = await SeedSampleTours(tourRepository, context);
            await SeedSampleServices(serviceRepository, tours, context);
            await SeedSampleUsers(userRepository, context);
            await SeedSampleBookings(bookRepository, tours, context);
        }

        private static async Task SeedAdminUser(IUserRepository userRepository, ApplicationDbContext context)
        {
            var adminUser = await userRepository.GetUserByNameAsync("admin");
            if (adminUser == null)
            {
                var admin = new User
                {
                    Username = "admin",
                    HashPassword = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                    Email = "admin@example.com",
                    Role = Role.Admin
                };

                await userRepository.AddNewUserAsync(admin);
                await context.SaveChangesAsync();
            }
        }

        private static async Task<List<Tour>> SeedSampleTours(ITourRepository tourRepository, ApplicationDbContext context)
        {
            var tours = await tourRepository.GetToursAsync();
            if (!tours.Any())
            {
                var sampleTours = new List<Tour>
                {
                    new Tour
                    {
                        Name = "Ha Long Bay Cruise",
                        Destination = "Ha Long Bay",
                        Price = 299.99m,
                        StartDate = DateTime.Now.AddDays(7),
                        EndDate = DateTime.Now.AddDays(10),
                        Description = "Experience the stunning beauty of Ha Long Bay with our luxury cruise package",
                        ImageUrl = "images/69b88aec-fdd1-4c8d-a885-82a87444d259.png"
                    },
                    new Tour
                    {
                        Name = "Hoi An Ancient Town",
                        Destination = "Hoi An",
                        Price = 149.99m,
                        StartDate = DateTime.Now.AddDays(5),
                        EndDate = DateTime.Now.AddDays(6),
                        Description = "Explore the historic charm of Hoi An Ancient Town",
                        ImageUrl = "images/69b88aec-fdd1-4c8d-a885-82a87444d259.png"
                    },
                    new Tour
                    {
                        Name = "Sapa Trekking",
                        Destination = "Sapa",
                        Price = 199.99m,
                        StartDate = DateTime.Now.AddDays(10),
                        EndDate = DateTime.Now.AddDays(12),
                        Description = "Trek through the beautiful terraced rice fields of Sapa",
                        ImageUrl = "images/69b88aec-fdd1-4c8d-a885-82a87444d259.png"
                    },
                    new Tour
                    {
                        Name = "Mekong Delta Explorer",
                        Destination = "Mekong Delta",
                        Price = 249.99m,
                        StartDate = DateTime.Now.AddDays(15),
                        EndDate = DateTime.Now.AddDays(17),
                        Description = "Discover the vibrant life along the Mekong Delta",
                        ImageUrl = "images/69b88aec-fdd1-4c8d-a885-82a87444d259.png"
                    },
                    new Tour
                    {
                        Name = "Hue Imperial City",
                        Destination = "Hue",
                        Price = 179.99m,
                        StartDate = DateTime.Now.AddDays(20),
                        EndDate = DateTime.Now.AddDays(21),
                        Description = "Visit the historic Imperial City of Hue",
                        ImageUrl = "images/69b88aec-fdd1-4c8d-a885-82a87444d259.png"
                    }
                };

                foreach (var tour in sampleTours)
                {
                    await tourRepository.AddTourAsync(tour);
                }
                await context.SaveChangesAsync();
                return sampleTours;
            }
            return tours.ToList();
        }

        private static async Task SeedSampleServices(IServiceRepository serviceRepository, List<Tour> tours, ApplicationDbContext context)
        {
            var services = await serviceRepository.GetServicesAsync();
            if (!services.Any())
            {
                var sampleServices = new List<Service>
                {
                    new Service
                    {
                        Name = "Airport Transfer",
                        Description = "Comfortable transfer from airport to hotel",
                        TourId = tours[0].Id
                    },
                    new Service
                    {
                        Name = "City Tour Guide",
                        Description = "Professional guide for city exploration",
                        TourId = tours[0].Id
                    },
                    new Service
                    {
                        Name = "Hotel Booking",
                        Description = "Assistance with hotel reservations",
                        TourId = tours[0].Id
                    },
                    new Service
                    {
                        Name = "Local Food Tour",
                        Description = "Experience authentic local cuisine",
                        TourId = tours[1].Id
                    },
                    new Service
                    {
                        Name = "Traditional Craft Workshop",
                        Description = "Learn traditional crafts from local artisans",
                        TourId = tours[1].Id
                    },
                    new Service
                    {
                        Name = "Mountain Guide",
                        Description = "Experienced guide for trekking",
                        TourId = tours[2].Id
                    },
                    new Service
                    {
                        Name = "Equipment Rental",
                        Description = "Trekking equipment rental service",
                        TourId = tours[2].Id
                    },
                    new Service
                    {
                        Name = "Boat Tour",
                        Description = "River cruise through Mekong Delta",
                        TourId = tours[3].Id
                    },
                    new Service
                    {
                        Name = "Floating Market Visit",
                        Description = "Visit traditional floating markets",
                        TourId = tours[3].Id
                    },
                    new Service
                    {
                        Name = "Historical Guide",
                        Description = "Expert guide for historical sites",
                        TourId = tours[4].Id
                    }
                };

                foreach (var service in sampleServices)
                {
                    await serviceRepository.AddServiceAsync(service);
                }
                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedSampleUsers(IUserRepository userRepository, ApplicationDbContext context)
        {
            var sampleUsers = new List<User>
            {
                new User
                {
                    Username = "john_doe",
                    HashPassword = BCrypt.Net.BCrypt.HashPassword("Password@123"),
                    Email = "john@example.com",
                    Role = Role.User
                },
                new User
                {
                    Username = "jane_smith",
                    HashPassword = BCrypt.Net.BCrypt.HashPassword("Password@123"),
                    Email = "jane@example.com",
                    Role = Role.User
                },
                new User
                {
                    Username = "mike_wilson",
                    HashPassword = BCrypt.Net.BCrypt.HashPassword("Password@123"),
                    Email = "mike@example.com",
                    Role = Role.User
                }
            };

            foreach (var user in sampleUsers)
            {
                var existingUser = await userRepository.GetUserByNameAsync(user.Username);
                if (existingUser == null)
                {
                    await userRepository.AddNewUserAsync(user);
                }
            }
            await context.SaveChangesAsync();
        }

        private static async Task SeedSampleBookings(IBookRepository bookRepository, List<Tour> tours, ApplicationDbContext context)
        {
            var bookings = await bookRepository.GetBooksAsync();
            if (!bookings.Any())
            {
                var users = context.Users.Where(u => u.Role == Role.User).ToList();
                var sampleBookings = new List<Book>
                {
                    new Book
                    {
                        TourId = tours[0].Id,
                        UserId = users[0].Id,
                        BookingDate = DateTime.Now,
                        Status = "Confirmed",
                        Quantity = 2
                    },
                    new Book
                    {
                        TourId = tours[1].Id,
                        UserId = users[1].Id,
                        BookingDate = DateTime.Now.AddDays(1),
                        Status = "Pending",
                        Quantity = 1
                    },
                    new Book
                    {
                        TourId = tours[2].Id,
                        UserId = users[2].Id,
                        BookingDate = DateTime.Now.AddDays(2),
                        Status = "Confirmed",
                        Quantity = 3
                    }
                };

                foreach (var booking in sampleBookings)
                {
                    await bookRepository.AddBookAsync(booking);
                }
                await context.SaveChangesAsync();
            }
        }
    }
} 