using Microsoft.AspNetCore.Mvc;
using Reto_0_Backend.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;


namespace Reto_0_Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class GeometryController : ControllerBase
{
   
    //private DataCollectionExample dataCollectionExample = new DataCollectionExample();

    private static List<Geometry> geometries = new List<Geometry>();
    private readonly ILogger<Geometry> _logger;

    public GeometryController(ILogger<Geometry> logger)
    {
        _logger = logger;
    }


    [HttpGet]
    public ActionResult<IEnumerable<Geometry>> GetGeometry()
    {
        return Ok(geometries);
    }

    [HttpGet("{id}")]
    public ActionResult<Geometry> GetGeometry(string id)
    {
        var geometry = geometries.FirstOrDefault(g => g.id == id);
        if (geometry == null)
        {
            return NotFound();
        }
        return Ok(geometry);
    }

    
    [HttpPost]
    public ActionResult<Geometry> CreateGeometry(Geometry newGeometry)
    {
        geometries.Add(newGeometry);
        return CreatedAtAction(nameof(newGeometry), new { id = newGeometry }, newGeometry);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateGeometry(string id, Geometry updatedGeometry)
    {
        var existingGeometry = geometries.FirstOrDefault(g => g.id == id);
        if (existingGeometry == null)
        {
            return NotFound();
        }
        existingGeometry.id = updatedGeometry.id;
        existingGeometry.type = updatedGeometry.type;
        existingGeometry.coordinates = updatedGeometry.coordinates;

        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public IActionResult DeleteGeomtry(string id)
    {
        var deleteGeometry = geometries.FirstOrDefault(g => g.id == id);
        if (deleteGeometry == null)
        {
            return NotFound();
        }
        geometries.Remove(deleteGeometry);
        return NoContent();
    }
}
