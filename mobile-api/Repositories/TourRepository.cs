using Microsoft.EntityFrameworkCore;
using mobile_api.Data;
using mobile_api.Models;
using mobile_api.Repositories.Interfaces;

namespace mobile_api.Repositories
{
    public class TourRepository : ITourRepository
    {
        private readonly ILogger<TourRepository> _logger;
        private readonly ApplicationDbContext _context;
        public TourRepository(ILogger<TourRepository> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public async Task<bool> AddTourAsync(Tour tour)
        {
            _logger.LogInformation($"{nameof(TourRepository)} action: {nameof(AddTourAsync)}");
            await _context.Tours.AddAsync(tour);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteTourAsync(string id)
        {
            _logger.LogInformation($"{nameof(TourRepository)} action: {nameof(DeleteTourAsync)}");
            var tour = await _context.Tours.FirstOrDefaultAsync(item => item.Id == id);
            if (tour == null)
            {
                return false;
            }
            _context.Tours.Remove(tour);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Tour> GetTourByIdAsync(string id)
        {
            _logger.LogInformation($"{nameof(TourRepository)} action: {nameof(GetTourByIdAsync)}");
            return await _context.Tours
                .AsNoTracking() // Prevent tracking
                .Where(item => item.Id == id)
                .Select(item => new Tour
                {
                    Id = item.Id,
                    Name = item.Name,
                    Destination = item.Destination,
                    Price = item.Price,
                    StartDate = item.StartDate,
                    EndDate = item.EndDate,
                    Description = item.Description,
                    ImageUrl = item.ImageUrl,
                    Categories = item.Categories.Select(c => new Category { Id = c.Id, Name = c.Name }).ToList(),
                    Tags = item.Tags.Select(t => new Tag { Id = t.Id, Name = t.Name }).ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Tour>> GetToursAsync()
        {
            _logger.LogDebug($"{nameof(TourRepository)} action: {nameof(GetToursAsync)}");
            return await _context.Tours
                .AsNoTracking() // Prevent tracking
                .Select(item => new Tour
                {
                    Id = item.Id,
                    Name = item.Name,
                    Destination = item.Destination,
                    Price = item.Price,
                    StartDate = item.StartDate,
                    EndDate = item.EndDate,
                    Description = item.Description,
                    ImageUrl = item.ImageUrl,
                    Categories = item.Categories.Select(c => new Category { Id = c.Id, Name = c.Name }).ToList(),
                    Tags = item.Tags.Select(t => new Tag { Id = t.Id, Name = t.Name }).ToList()
                })
                .ToListAsync();
        }

        public async Task<bool> UpdateTourAsync(Tour tour)
        {
            _logger.LogInformation($"{nameof(TourRepository)} action: {nameof(UpdateTourAsync)}");

            var existingTour = await _context.Tours
                .Include(t => t.Categories)
                .Include(t => t.Tags)
                .FirstOrDefaultAsync(t => t.Id == tour.Id);

            if (existingTour == null)
            {
                return false;
            }

            // Update basic properties
            existingTour.Name = tour.Name;
            existingTour.Destination = tour.Destination;
            existingTour.Price = tour.Price;
            existingTour.StartDate = tour.StartDate;
            existingTour.EndDate = tour.EndDate;
            existingTour.Description = tour.Description;
            existingTour.ImageUrl = tour.ImageUrl;

            // Update Categories
            existingTour.Categories.Clear();
            foreach (var category in tour.Categories)
            {
                var existingCategory = await _context.Categories.FindAsync(category.Id);
                if (existingCategory != null)
                {
                    existingTour.Categories.Add(existingCategory);
                }
            }

            // Update Tags
            existingTour.Tags.Clear();
            foreach (var tag in tour.Tags)
            {
                var existingTag = await _context.Tags.FindAsync(tag.Id);
                if (existingTag != null)
                {
                    existingTour.Tags.Add(existingTag);
                }
            }

            _context.Tours.Update(existingTour);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateTourCategoriesAndTagsAsync(string tourId, List<int> categoryIds, List<int> tagIds)
        {
            _logger.LogInformation($"{nameof(TourRepository)} action: {nameof(UpdateTourCategoriesAndTagsAsync)}");

            var tour = await _context.Tours
                .Include(t => t.Categories)
                .Include(t => t.Tags)
                .FirstOrDefaultAsync(t => t.Id == tourId);

            if (tour == null)
            {
                return false;
            }

            // Update Categories
            tour.Categories.Clear();
            foreach (var categoryId in categoryIds)
            {
                var category = await _context.Categories.FindAsync(categoryId);
                if (category != null)
                {
                    tour.Categories.Add(category);
                }
            }

            // Update Tags
            tour.Tags.Clear();
            foreach (var tagId in tagIds)
            {
                var tag = await _context.Tags.FindAsync(tagId);
                if (tag != null)
                {
                    tour.Tags.Add(tag);
                }
            }

            _context.Tours.Update(tour);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
