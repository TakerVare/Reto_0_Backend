using Microsoft.AspNetCore.Mvc;
using Reto_0_Backend.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;


namespace Reto_0_Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{
    private readonly DataCollectionExample _dataCollection;
    private readonly ILogger<CategoryController> _logger;

    public CategoryController(ILogger<CategoryController> logger, DataCollectionExample dataCollection)
    {
        _logger = logger;
        _dataCollection = dataCollection;
    }


    [HttpGet]
    public ActionResult<IEnumerable<Category>> GetCategory()
    {
        return Ok(_dataCollection.CategoryCollectionList);
    }

    [HttpGet("{id}")]
    public ActionResult<Category> GetCategory(string id)
    {
        var Category = _dataCollection.CategoryCollectionList.FirstOrDefault(cat => cat.idCategory == id);
        if (Category == null)
        {
            return NotFound();
        }
        return Ok(Category);
    }

    [HttpPost]
    public ActionResult<Category> CreateCategory(Category newCategory)
    {
        _dataCollection.CategoryCollectionList.Add(newCategory);
        return CreatedAtAction(nameof(GetCategory), new { id = newCategory.idCategory }, newCategory);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateCategory(string id, Category updatedCategory)
    {
        var category = _dataCollection.CategoryCollectionList.FirstOrDefault(cat => cat.idCategory == id);
        if (category == null)
        {
            return NotFound();
        }
        category.idCategory = updatedCategory.idCategory;
        category.titleCategory = updatedCategory.titleCategory;
        category.linkCategory = updatedCategory.linkCategory;
        category.descriptionCategory = updatedCategory.descriptionCategory;
        category.layersCategory = updatedCategory.layersCategory;

        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public IActionResult DeleteCategory(string id)
    {
        var category = _dataCollection.CategoryCollectionList.FirstOrDefault(cat => cat.idCategory == id);
        if (category == null)
        {
            return NotFound();
        }
        _dataCollection.CategoryCollectionList.Remove(category);
        return NoContent();
    }
}