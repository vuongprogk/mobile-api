using mobile_api.Models;

namespace mobile_api.Repositories.Interfaces;

public interface ITourReleventRepository
{
    public Task<bool> AddTag(Tag tag);
    public Task<bool> UpdateTag(Tag tag);
    public Task<bool> DeleteTag(Tag tag);
    public Task<IEnumerable<Tag>> GetAllTags();
    public Task<IEnumerable<Tag>> GetTagsByName(string name);
    public Task<bool> AddCategory(Category category);
    public Task<bool> UpdateCategory(Category category);
    public Task<bool> DeleteCategory(Category category);
    public Task<IEnumerable<Category>> GetAllCategories();
    public Task<IEnumerable<Category>> GetCategoriesByName(string name);
}