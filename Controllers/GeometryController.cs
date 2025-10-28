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
public class GeometryController : ControllerBase
{
    private static List<Geometry> geometries = new List<Geometry>();
    private readonly IGeometryRepository _repository;
    public GeometryController(IGeometryRepository repository) {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<List<Geometry>>> GetGeometry()
    {
        var geometries = await _repository.GetAllAsync();
        return Ok(geometries);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Geometry>> GetGeometry(string id)
    {
        var geometry = await _repository.GetByIdAsync(id);
        if (geometry == null)
        {
            return NotFound();
        }
        return Ok(geometry);
    }

    
    [HttpPost]
    public async Task<ActionResult<Geometry>> CreateGeometry(Geometry newGeometry)
    {
       await _repository.AddAsync(newGeometry);
        return CreatedAtAction(nameof(GetGeometry), new { id = newGeometry.id }, newGeometry);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGeometry(string id, Geometry updatedGeometry)
    {
        var existingGeometry = await _repository.GetByIdAsync(id);
        if (existingGeometry == null)
        {
            return NotFound();
        }
        existingGeometry.id = updatedGeometry.id;
        existingGeometry.type = updatedGeometry.type;
        existingGeometry.coordinates = updatedGeometry.coordinates;
        

        await _repository.UpdateAsync(existingGeometry);
        return NoContent();
    }
    
    [HttpDelete("{id}")]
     public async Task<IActionResult>  DeleteGeomtry(string id)
    {
        var deleteGeometry = await _repository.GetByIdAsync(id);
        if (deleteGeometry == null)
        {
            return NotFound();
        }
        await _repository.DeleteAsync(id);
        return NoContent();
    }
}