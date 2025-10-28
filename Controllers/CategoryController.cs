using Microsoft.AspNetCore.Mvc;
using Reto_0_Backend.Models;
using Reto_0_Backend.Repositories;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Reto_0_Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private static List<Category> categories = new List<Category>();
    private readonly ICategoryRepository _repository;

    
    public CategoryController(ICategoryRepository repository) {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<List<Category>>> GetCategory()
    {
        var categories = await _repository.GetAllAsync();
        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Category>> GetCategory(string id)
    {
        var category = await _repository.GetByIdAsync(id);
        if (category == null)
        {
            return NotFound();
        }
        return Ok(category);
    }

    [HttpPost]
    public async Task<ActionResult<Category>> CreateCategory(Category newCategory)
    {
        await _repository.AddAsync(newCategory);
        return CreatedAtAction(nameof(GetCategory), new { id = newCategory.idCategory }, newCategory);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(string id, Category updatedCategory)
    {
        var existingCategory = await _repository.GetByIdAsync(id);
        if (existingCategory == null)
        {
            return NotFound();
        }
        existingCategory.idCategory = updatedCategory.idCategory;
        existingCategory.titleCategory = updatedCategory.titleCategory;
        existingCategory.linkCategory = updatedCategory.linkCategory;
        existingCategory.descriptionCategory = updatedCategory.descriptionCategory;
        existingCategory.layersCategory = updatedCategory.layersCategory;

        await _repository.UpdateAsync(existingCategory);
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(string id)
    {
        var category = await _repository.GetByIdAsync(id);
        if (category == null)
        {
            return NotFound();
        }
        await _repository.DeleteAsync(id);
        return NoContent();
    }
}