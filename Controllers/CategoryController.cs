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
   
    //private DataCollectionExample dataCollectionExample = new DataCollectionExample();

    private static List<Category> categories = new List<Category>();
    private readonly ILogger<CategoryController> _logger;

    public CategoryController(ILogger<CategoryController> logger)
    {
        _logger = logger;
    }


    [HttpGet]
    public ActionResult<IEnumerable<Category>> GetCategory()
    {
        return Ok(categories);
    }

    [HttpGet("{id}")]
    public ActionResult<Category> GetCategory(string id)
    {
        var Category = categories.FirstOrDefault(cat => cat.idCategory == id);
        if (Category == null)
        {
            return NotFound();
        }
        return Ok(Category);
    }

    /*
    public IEnumerable<Category> Get()
    {

        return dataCollectionExample.CategoryCollectionList.ToArray();

    }
    */
    [HttpPost]
    public ActionResult<Category> CreateCategory(Category newCategory)
    {
        categories.Add(newCategory);
        return CreatedAtAction(nameof(GetCategory), new { id = newCategory.idCategory }, newCategory);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateCategory(string id, Category updatedCategory)
    {
        var category = categories.FirstOrDefault(cat => cat.idCategory == id);
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
        var category = categories.FirstOrDefault(cat => cat.idCategory == id);
        if (category == null)
        {
            return NotFound();
        }
        categories.Remove(category);
        return NoContent();
    }
}
