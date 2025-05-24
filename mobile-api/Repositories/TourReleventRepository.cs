using mobile_api.Data;
using mobile_api.Models;
using mobile_api.Repositories.Interfaces;

namespace mobile_api.Repositories;

public class TourReleventRepository(ILogger<TourReleventRepository> logger, ApplicationDbContext context)
    : ITourReleventRepository
{
    private readonly ILogger<TourReleventRepository> _logger = logger;
    private readonly ApplicationDbContext _context = context;

    public async Task<bool> AddTag(Tag tag)
    {
        _logger.LogInformation($"{nameof(TourReleventRepository)} action: {nameof(AddTag)}");
        await _context.Tags.AddAsync(tag);
        return await _context.SaveChangesAsync() > 0;

    }

    public async Task<bool> UpdateTag(Tag tag)
    {
        _logger.LogInformation($"{nameof(TourReleventRepository)} action: {nameof(UpdateTag)}");
        _context.Tags.Update(tag);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteTag(Tag tag)
    {
        _logger.LogInformation($"{nameof(TourReleventRepository)} action: {nameof(DeleteTag)}");
        _context.Tags.Remove(tag);
        return await _context.SaveChangesAsync() > 0;
    }

    public Task<IEnumerable<Tag>> GetAllTags()
    {
        _logger.LogInformation($"{nameof(TourReleventRepository)} action: {nameof(GetAllTags)}");
        return Task.FromResult<IEnumerable<Tag>>(_context.Tags);
    }

    public Task<IEnumerable<Tag>> GetTagsByName(string name)
    {
        _logger.LogInformation($"{nameof(TourReleventRepository)} action: {nameof(GetTagsByName)}");
        return Task.FromResult<IEnumerable<Tag>>(_context.Tags.Where(t => t.Name == name).ToList());
    }

    public async Task<bool> AddCategory(Category category)
    {
        _logger.LogInformation($"{nameof(TourReleventRepository)} action: {nameof(AddCategory)}");
        await _context.Categories.AddAsync(category);
        return  await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateCategory(Category category)
    {
        _logger.LogInformation($"{nameof(TourReleventRepository)} action: {nameof(UpdateCategory)}");
        _context.Categories.Update(category);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteCategory(Category category)
    {
        _logger.LogInformation($"{nameof(TourReleventRepository)} action: {nameof(DeleteCategory)}");
        _context.Categories.Remove(category);
        return await _context.SaveChangesAsync() > 0;
    }

    public Task<IEnumerable<Category>> GetAllCategories()
    {
        _logger.LogInformation($"{nameof(TourReleventRepository)} action: {nameof(GetAllCategories)}");
        return Task.FromResult<IEnumerable<Category>>(_context.Categories);
    }

    public Task<IEnumerable<Category>> GetCategoriesByName(string name)
    {
        _logger.LogInformation($"{nameof(TourReleventRepository)} action: {nameof(GetCategoriesByName)}");
        return Task.FromResult<IEnumerable<Category>>(_context.Categories.Where(t => t.Name == name).ToList());
    }
}