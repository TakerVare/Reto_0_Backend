using Microsoft.AspNetCore.Mvc;
using Reto_0_Backend.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;


namespace Reto_0_Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class LayerController : ControllerBase
{
   
    //private DataCollectionExample dataCollectionExample = new DataCollectionExample();

    private static List<Layer> layers = new List<Layer>();
    private readonly ILogger<Layer> _logger;

    public LayerController(ILogger<Layer> logger)
    {
        _logger = logger;
    }


    [HttpGet]
    public ActionResult<IEnumerable<Layer>> GetLayers()
    {
        return Ok(layers);
    }

    [HttpGet("{id}")]
    public ActionResult<Layer> GetLayer(string id)
    {
        var layer = layers.FirstOrDefault(l => l.id == id);
        if (layer == null)
        {
            return NotFound();
        }
        return Ok(layer);
    }

    
    [HttpPost]
    public ActionResult<Layer> CreateLayer(Layer newLayer)
    {
        layers.Add(newLayer);
        return CreatedAtAction(nameof(newLayer), new { id = newLayer }, newLayer);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateLayer(string id, Layer updatedLayer)
    {
        var existingLayer = layers.FirstOrDefault(l => l.id == id);
        if (existingLayer == null)
        {
            return NotFound();
        }
        existingLayer.id = updatedLayer.id;
        existingLayer.name = updatedLayer.name;
        existingLayer.serviceUrl = updatedLayer.serviceUrl;
        existingLayer.serviceTypeId = updatedLayer.serviceTypeId;
        existingLayer.parameters = updatedLayer.parameters;
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public IActionResult DeleteLayer(string id)
    {
        var deleteLayer = layers.FirstOrDefault(l => l.id == id);
        if (deleteLayer == null)
        {
            return NotFound();
        }
        layers.Remove(deleteLayer);
        return NoContent();
    }
}
