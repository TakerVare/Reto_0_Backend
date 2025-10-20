using Microsoft.AspNetCore.Mvc;
using Reto_0_Backend.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;


namespace Reto_0_Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class SourceController : ControllerBase
{
   
    //private DataCollectionExample dataCollectionExample = new DataCollectionExample();

    private static List<Source> sources = new List<Source>();
    private readonly ILogger<Source> _logger;

    public SourceController(ILogger<Source> logger)
    {
        _logger = logger;
    }


    [HttpGet]
    public ActionResult<IEnumerable<Source>> GetLayers()
    {
        return Ok(sources);
    }

    [HttpGet("{id}")]
    public ActionResult<Source> GetSource(string id)
    {
        var source = sources.FirstOrDefault(s => s.id == id);
        if (source == null)
        {
            return NotFound();
        }
        return Ok(source);
    }

    
    [HttpPost]
    public ActionResult<Source> CreateSource(Source newSource)
    {
        sources.Add(newSource);
        return CreatedAtAction(nameof(newSource), new { id = newSource }, newSource);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateSource(string id, Source updatedSource)
    {
        var existingSource = sources.FirstOrDefault(s => s.id == id);
        if (existingSource == null)
        {
            return NotFound();
        }
        existingSource.id = updatedSource.id;
        existingSource.url = updatedSource.url;
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public IActionResult DeleteSource(string id)
    {
        var deleteSource = sources.FirstOrDefault(s => s.id == id);
        if (deleteSource == null)
        {
            return NotFound();
        }
        sources.Remove(deleteSource);
        return NoContent();
    }
}
