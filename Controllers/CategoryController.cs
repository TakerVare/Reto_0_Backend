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
   
    private DataCollectionExample dataCollectionExample = new DataCollectionExample();

    private readonly ILogger<CategoryController> _logger;

    public CategoryController(ILogger<CategoryController> logger)
    {
        _logger = logger;
    }

    
    [HttpGet]
    public  IEnumerable<Category> Get()
    {
                
        return dataCollectionExample.CategoryCollectionList.ToArray();
        
        /*
        return Enumerable.Range(1, 5).Select(index => new Category
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
        */
    }
}
