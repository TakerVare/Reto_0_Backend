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
    private readonly DataCollectionExample _dataCollection;
    private readonly ILogger<Layer> _logger;

    public LayerController(ILogger<Layer> logger, DataCollectionExample dataCollection)
    {
        _logger = logger;
        _dataCollection = dataCollection;
    }


    [HttpGet]
    public ActionResult<IEnumerable<Layer>> GetLayers()
    {
        return Ok(_dataCollection.LayerCollectionList);
    }

    [HttpGet("{id}")]
    public ActionResult<Layer> GetLayer(string id)
    {
        var layer = _dataCollection.LayerCollectionList.FirstOrDefault(l => l.id == id);
        if (layer == null)
        {
            return NotFound();
        }
        return Ok(layer);
    }


    [HttpPost]
    public ActionResult<Layer> CreateLayer(Layer newLayer)
    {
        _dataCollection.LayerCollectionList.Add(newLayer);
        return CreatedAtAction(nameof(GetLayer), new { id = newLayer.id }, newLayer);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateLayer(string id, Layer updatedLayer)
    {
        var existingLayer = _dataCollection.LayerCollectionList.FirstOrDefault(l => l.id == id);
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
        var deleteLayer = _dataCollection.LayerCollectionList.FirstOrDefault(l => l.id == id);
        if (deleteLayer == null)
        {
            return NotFound();
        }
        _dataCollection.LayerCollectionList.Remove(deleteLayer);
        return NoContent();
    }
}