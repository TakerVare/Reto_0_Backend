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
public class PropertyController : ControllerBase
{
    private static List<Property> properties = new List<Property>();
    private readonly IPropertyRepository _repository;

    
    public PropertyController(IPropertyRepository repository) {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<List<Property>>> GetProperties()
    {
        var properties = await _repository.GetAllAsync();
        return Ok(properties);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Property>> GetProperty(string id)
    {
        var property = await _repository.GetByIdAsync(id);
        if (property == null)
        {
            return NotFound();
        }
        return Ok(property);
    }

    
    [HttpPost]
    public async Task<ActionResult<Property>> CreateProperty(Property newProperty)
    {
        await _repository.AddAsync(newProperty);
        return CreatedAtAction(nameof(GetProperty), new { id = newProperty.id }, newProperty);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProperty(string id, Property updatedProperty)
    {
        var existingProperty = await _repository.GetByIdAsync(id);
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
        await _repository.UpdateAsync(existingProperty);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProperty(string id)
    {
        var deleteProperty = await _repository.GetByIdAsync(id);
        if (deleteProperty == null)
        {
            return NotFound();
        }
        await _repository.DeleteAsync(id);
        return NoContent();
    }

   
}