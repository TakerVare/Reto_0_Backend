using Microsoft.AspNetCore.Mvc;
using Reto_0_Backend.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;


namespace Reto_0_Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class PropertyController : ControllerBase
{
   
    //private DataCollectionExample dataCollectionExample = new DataCollectionExample();

    private static List<Property> properties = new List<Property>();
    private readonly ILogger<Property> _logger;

    public PropertyController(ILogger<Property> logger)
    {
        _logger = logger;
    }


    [HttpGet]
    public ActionResult<IEnumerable<Property>> GetProperties()
    {
        return Ok(properties);
    }

    [HttpGet("{id}")]
    public ActionResult<Property> GetProperty(string id)
    {
        var property = properties.FirstOrDefault(p => p.id == id);
        if (property == null)
        {
            return NotFound();
        }
        return Ok(property);
    }

    
    [HttpPost]
    public ActionResult<Property> CreateProperty(Property newProperty)
    {
        properties.Add(newProperty);
        return CreatedAtAction(nameof(newProperty), new { id = newProperty }, newProperty);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateProperty(string id, Property updatedProperty)
    {
        var existingProperty = properties.FirstOrDefault(p => p.id == id);
        if (existingProperty == null)
        {
            return NotFound();
        }
        existingProperty.id = updatedProperty.id;
        existingProperty.title = updatedProperty.title;
        existingProperty.description = updatedProperty.description;
        existingProperty.link = updatedProperty.link;
        existingProperty.closed = updatedProperty.closed;
        existingProperty.date = updatedProperty.date;
        existingProperty.magnitudeValue = updatedProperty.magnitudeValue;
        existingProperty.magnitudeUnit = updatedProperty.magnitudeUnit;
        existingProperty.categories = updatedProperty.categories;
        existingProperty.sources = updatedProperty.sources;
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteProperty(string id)
    {
        var deleteProperty = properties.FirstOrDefault(p => p.id == id);
        if (deleteProperty == null)
        {
            return NotFound();
        }
        properties.Remove(deleteProperty);
        return NoContent();
    }

   
}
