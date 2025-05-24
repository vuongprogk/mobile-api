using Microsoft.AspNetCore.Mvc;
using mobile_api.Models;
using mobile_api.Repositories.Interfaces;

namespace mobile_api.Controllers;

[ApiController]
[Route("/api/TourRelevent")]
public class TourRelevent(ITourReleventRepository repository1) : ControllerBase
{
    private readonly ITourReleventRepository _repository = repository1;

    // Tags Endpoints
    [HttpPost("tags")]
    public async Task<IActionResult> AddTag([FromBody] Tag tag)
    {
        var result = await _repository.AddTag(tag);
        if (result) return Ok();
        return BadRequest();
    }

    [HttpPut("tags")]
    public async Task<IActionResult> UpdateTag([FromBody] Tag tag)
    {
        var result = await _repository.UpdateTag(tag);
        if (result) return Ok();
        return BadRequest();
    }

    [HttpDelete("tags")]
    public async Task<IActionResult> DeleteTag([FromBody] Tag tag)
    {
        var result = await _repository.DeleteTag(tag);
        if (result) return Ok();
        return BadRequest();
    }

    [HttpGet("tags")]
    public IActionResult GetAllTags()
    {
        var tags = _repository.GetAllTags();
        return Ok(tags);
    }

    [HttpGet("tags/search")]
    public IActionResult GetTagsByName([FromQuery] string name)
    {
        var tags = _repository.GetTagsByName(name);
        return Ok(tags);
    }

    // Categories Endpoints
    [HttpPost("categories")]
    public async Task<IActionResult> AddCategory([FromBody] Category category)
    {
        var result = await _repository.AddCategory(category);
        if (result) return Ok();
        return BadRequest();
    }

    [HttpPut("categories")]
    public async Task<IActionResult> UpdateCategory([FromBody] Category category)
    {
        var result = await _repository.UpdateCategory(category);
        if (result) return Ok();
        return BadRequest();
    }

    [HttpDelete("categories")]
    public async Task<IActionResult> DeleteCategory([FromBody] Category category)
    {
        var result = await _repository.DeleteCategory(category);
        if (result) return Ok();
        return BadRequest();
    }

    [HttpGet("categories")]
    public IActionResult GetAllCategories()
    {
        var categories = _repository.GetAllCategories();
        return Ok(categories);
    }

    [HttpGet("categories/search")]
    public IActionResult GetCategoriesByName([FromQuery] string name)
    {
        var categories = _repository.GetCategoriesByName(name);
        return Ok(categories);
    }
}