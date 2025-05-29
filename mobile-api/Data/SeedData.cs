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

            // Seed tags and categories
            //await SeedSampleTagsAndCategories(context);

            // Seed sample data
            var tours = await SeedSampleTours(tourRepository, context, serviceRepository);
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

        private static async Task<List<Tour>> SeedSampleTours(ITourRepository tourRepository, ApplicationDbContext context, IServiceRepository serviceRepository)
        {
            if (!context.Services.Any())
            {
                var sampleServices = new List<Service>
                {
                    new Service
                    {
                        Name = "Airport Transfer",
                        Description = "Comfortable transfer from airport to hotel",
                        Price = 12,
                    },
                    new Service
                    {
                        Name = "City Tour Guide",
                        Description = "Professional guide for city exploration",
                        Price = 25,
                    },
                    new Service
                    {
                        Name = "Hotel Booking",
                        Description = "Assistance with hotel reservations",
                        Price = 50,
                    },
                    new Service
                    {
                        Name = "Local Food Tour",
                        Description = "Experience authentic local cuisine",
                        Price = 30,
                    },
                    new Service
                    {
                        Name = "Traditional Craft Workshop",
                        Description = "Learn traditional crafts from local artisans",
                        Price = 40,
                    },
                    new Service
                    {
                        Name = "Mountain Guide",
                        Description = "Experienced guide for trekking",
                        Price = 60,
                    },
                    new Service
                    {
                        Name = "Equipment Rental",
                        Description = "Trekking equipment rental service",
                        Price = 20,
                    },
                    new Service
                    {
                        Name = "Boat Tour",
                        Description = "River cruise through Mekong Delta",
                        Price = 70,
                    },
                    new Service
                    {
                        Name = "Floating Market Visit",
                        Description = "Visit traditional floating markets",
                        Price = 15,
                    },
                    new Service
                    {
                        Name = "Historical Guide",
                        Description = "Expert guide for historical sites",
                        Price = 35,
                    }
                };

                context.Services.AddRange(sampleServices);
            }
            if (!context.Tags.Any())
            {
                var tags = new List<Tag>
                {
                    new Tag { Name = "Beach" },
                    new Tag { Name = "Mountain" },
                    new Tag { Name = "City" },
                    new Tag { Name = "Adventure" },
                    new Tag { Name = "Relaxation" }
                };

                context.Tags.AddRange(tags);
            }

            if (!context.Categories.Any())
            {
                var categories = new List<Category>
                {
                    new Category { Name = "Adventure" },
                    new Category { Name = "Cultural" },
                    new Category { Name = "Nature" },
                    new Category { Name = "Luxury" },
                    new Category { Name = "Budget" }
                };

                context.Categories.AddRange(categories);
            }

            await context.SaveChangesAsync();

            var beachTag = context.Tags.FirstOrDefault(t => t.Name == "Beach");
            var mountainTag = context.Tags.FirstOrDefault(t => t.Name == "Mountain");
            var adventureCategory = context.Categories.FirstOrDefault(c => c.Name == "Adventure");
            var culturalCategory = context.Categories.FirstOrDefault(c => c.Name == "Cultural");
            var airportTransferService = context.Services.FirstOrDefault(s => s.Name == "Airport Transfer");
            var cityTourGuideService = context.Services.FirstOrDefault(s => s.Name == "City Tour Guide");
            var hotelBookingService = context.Services.FirstOrDefault(s => s.Name == "Hotel Booking");

            if (!context.Tours.Any())
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
                        ImageUrl = "images/halong_bay.jpg",
                        Categories = new List<Category> { adventureCategory },
                        Tags = new List<Tag> { beachTag },
                        Services = new List<Service>()
                        {
                            airportTransferService,
                            cityTourGuideService,
                            hotelBookingService
                        }
                    },
                    new Tour
                    {
                        Name = "Sapa Trekking",
                        Destination = "Sapa",
                        Price = 199.99m,
                        StartDate = DateTime.Now.AddDays(10),
                        EndDate = DateTime.Now.AddDays(12),
                        Description = "Trek through the beautiful terraced rice fields of Sapa",
                        ImageUrl = "images/sapa_trekking.jpg",
                        Categories = new List<Category> { adventureCategory, culturalCategory },
                        Tags = new List<Tag> { mountainTag },
                        Services = new List<Service>()
                        {
                            airportTransferService,
                            cityTourGuideService,
                            hotelBookingService
                        }
                    },
                    new Tour
                    {
                        Name = "Mekong Delta Adventure",
                        Destination = "Mekong Delta",
                        Price = 149.99m,
                        StartDate = DateTime.Now.AddDays(15),
                        EndDate = DateTime.Now.AddDays(18),
                        Description = "Explore the vibrant culture of the Mekong Delta",
                        ImageUrl = "images/mekong_delta.jpg",
                        Categories = new List<Category> { adventureCategory },
                        Tags = new List<Tag> {  },
                        Services = new List<Service>()
                        {
                            airportTransferService,
                            cityTourGuideService,
                            hotelBookingService
                        }
                    },
                    new Tour
                    {
                        Name = "Hue Imperial City Tour",
                        Destination = "Hue",
                        Price = 99.99m,
                        StartDate = DateTime.Now.AddDays(20),
                        EndDate = DateTime.Now.AddDays(21),
                        Description = "Discover the history of the Imperial City of Hue",
                        ImageUrl = "images/hue_city.jpg",
                        Categories = new List<Category> { culturalCategory },
                        Tags = new List<Tag> { mountainTag },
                        Services = new List<Service>()
                        {
                            airportTransferService,
                            cityTourGuideService,
                            hotelBookingService
                        }                    },
                    new Tour
                    {
                        Name = "Phu Quoc Island Getaway",
                        Destination = "Phu Quoc",
                        Price = 399.99m,
                        StartDate = DateTime.Now.AddDays(25),
                        EndDate = DateTime.Now.AddDays(30),
                        Description = "Relax on the beautiful beaches of Phu Quoc Island",
                        ImageUrl = "images/phu_quoc.jpg",
                        Categories = new List<Category> { adventureCategory },
                        Tags = new List<Tag> { beachTag },
                        Services = new List<Service>()
                        {
                            airportTransferService,
                            cityTourGuideService,
                            hotelBookingService
                        }
                    },
                    new Tour
                    {
                        Name = "Da Nang City Tour",
                        Destination = "Da Nang",
                        Price = 89.99m,
                        StartDate = DateTime.Now.AddDays(5),
                        EndDate = DateTime.Now.AddDays(6),
                        Description = "Explore the beautiful city of Da Nang with its beaches and mountains",
                        ImageUrl = "images/da_nang.jpg",
                        Categories = new List<Category> { culturalCategory },
                        Tags = new List<Tag> { mountainTag },
                        Services = new List<Service>()
                        {
                            airportTransferService,
                            cityTourGuideService,
                            hotelBookingService
                        }
                    },
                    new Tour
                    {
                        Name = "Nha Trang Beach Resort",
                        Destination = "Nha Trang",
                        Price = 299.99m,
                        StartDate = DateTime.Now.AddDays(12),
                        EndDate = DateTime.Now.AddDays(15),
                        Description = "Enjoy a luxurious beach resort experience in Nha Trang",
                        ImageUrl = "images/nha_trang.jpg",
                        Categories = new List<Category> { adventureCategory },
                        Tags = new List<Tag> { beachTag },
                        Services = new List<Service>()
                        {
                            airportTransferService,
                            cityTourGuideService,
                            hotelBookingService
                        }
                    },
                    new Tour
                    {
                        Name = "Phong Nha Cave Exploration",
                        Destination = "Phong Nha",
                        Price = 179.99m,
                        StartDate = DateTime.Now.AddDays(22),
                        EndDate = DateTime.Now.AddDays(24),
                        Description = "Discover the stunning caves of Phong Nha",
                        ImageUrl = "images/phong_nha.jpg",
                        Categories = new List<Category> { adventureCategory },
                        Tags = new List<Tag> { mountainTag },
                        Services = new List<Service>()
                        {
                            airportTransferService,
                            cityTourGuideService,
                            hotelBookingService
                        }
                    },
                    new Tour
                    {
                        Name = "Hanoi City Highlights",
                        Destination = "Hanoi",
                        Price = 79.99m,
                        StartDate = DateTime.Now.AddDays(3),
                        EndDate = DateTime.Now.AddDays(4),
                        Description = "Explore the rich history and culture of Hanoi",
                        ImageUrl = "images/hanoi_city.jpg",
                        Categories = new List<Category> { culturalCategory },
                        Tags = new List<Tag> { mountainTag },
                        Services = new List<Service>()
                        {
                            airportTransferService,
                            cityTourGuideService,
                            hotelBookingService
                        }
                    },
                    new Tour
                    {
                        Name = "Hoi An Ancient Town",
                        Destination = "Hoi An",
                        Price = 89.99m,
                        StartDate = DateTime.Now.AddDays(8),
                        EndDate = DateTime.Now.AddDays(9),
                        Description = "Experience the charm of Hoi An's ancient town",
                        ImageUrl = "images/hoi_an.jpg",
                        Categories = new List<Category> { culturalCategory },
                        Tags = new List<Tag> { mountainTag },
                        Services = new List<Service>()
                        {
                            airportTransferService,
                            cityTourGuideService,
                            hotelBookingService
                        }
                    },
                    new Tour
                    {
                        Name = "Con Dao Island Retreat",
                        Destination = "Con Dao",
                        Price = 499.99m,
                        StartDate = DateTime.Now.AddDays(28),
                        EndDate = DateTime.Now.AddDays(35),
                        Description = "A serene retreat on the beautiful Con Dao Island",
                        ImageUrl = "images/con_dao.jpg",
                        Categories = new List<Category> { adventureCategory },
                        Tags = new List<Tag> { beachTag },
                        Services = new List<Service>()
                        {
                            airportTransferService,
                            cityTourGuideService,
                            hotelBookingService
                        }
                    },
                    new Tour
                    {
                        Name = "Cat Ba Island Adventure",
                        Destination = "Cat Ba Island",
                        Price = 249.99m,
                        StartDate = DateTime.Now.AddDays(14),
                        EndDate = DateTime.Now.AddDays(16),
                        Description = "Adventure awaits on Cat Ba Island with trekking and kayaking",
                        ImageUrl = "images/cat_ba.jpg",
                        Categories = new List<Category> { adventureCategory },
                        Tags = new List<Tag> { mountainTag },
                        Services = new List<Service>()
                        {
                            airportTransferService,
                            cityTourGuideService,
                            hotelBookingService
                        }
                    },
                    new Tour
                    {
                        Name = "My Son Sanctuary Tour",
                        Destination = "My Son",
                        Price = 69.99m,
                        StartDate = DateTime.Now.AddDays(18),
                        EndDate = DateTime.Now.AddDays(19),
                        Description = "Explore the ancient ruins of My Son Sanctuary",
                        ImageUrl = "images/my_son.png",
                        Categories = new List<Category> { culturalCategory },
                        Tags = new List<Tag> { mountainTag },
                        Services = new List<Service>()
                        {
                            airportTransferService,
                            cityTourGuideService,
                            hotelBookingService
                        }
                    }

                };

                context.Tours.AddRange(sampleTours);
                await context.SaveChangesAsync();
            }

            return context.Tours.ToList();
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
                },
                new User
                {
                    Username = "alice_wonder",
                    HashPassword = BCrypt.Net.BCrypt.HashPassword("Password@123"),
                    Email = "alice@example.com",
                    Role = Role.User
                },
                new User
                {
                    Username = "bob_builder",
                    HashPassword = BCrypt.Net.BCrypt.HashPassword("Password@123"),
                    Email = "bob@example.com",
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
                        TourId = tours.FirstOrDefault(t => t.Name == "Ha Long Bay Cruise")?.Id,
                        UserId = users.FirstOrDefault()?.Id,
                        BookingDate = DateTime.Now,
                        Status = "Confirmed",
                        Quantity = 2
                    },
                    new Book
                    {
                        TourId = tours.FirstOrDefault(t => t.Name == "Sapa Trekking")?.Id,
                        UserId = users.Skip(1).FirstOrDefault()?.Id,
                        BookingDate = DateTime.Now.AddDays(1),
                        Status = "Pending",
                        Quantity = 1
                    },
                    new Book
                    {
                        TourId = tours.FirstOrDefault(t => t.Name == "Mekong Delta Adventure")?.Id,
                        UserId = users.Skip(2).FirstOrDefault()?.Id,
                        BookingDate = DateTime.Now.AddDays(2),
                        Status = "Confirmed",
                        Quantity = 3
                    },
                    new Book
                    {
                        TourId = tours.FirstOrDefault(t => t.Name == "Hue Imperial City Tour")?.Id,
                        UserId = users.FirstOrDefault(u => u.Username == "alice_wonder")?.Id,
                        BookingDate = DateTime.Now.AddDays(3),
                        Status = "Confirmed",
                        Quantity = 1
                    }
                };

                foreach (var booking in sampleBookings)
                {
                    if (booking.TourId != null && booking.UserId != null) // Ensure valid TourId and UserId
                    {
                        await bookRepository.AddBookAsync(booking);
                    }
                }
                await context.SaveChangesAsync();
            }
        }

    
    }
}
