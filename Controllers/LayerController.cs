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
public class LayerController : ControllerBase
{
    private static List<Layer> layers = new List<Layer>();
    private readonly ILayerRepository _repository;

    
    public LayerController(ILayerRepository repository) {
        _repository = repository;
    }


    [HttpGet]
    public async Task<ActionResult<List<Layer>>> GetLayers()
    {
        var layers = await _repository.GetAllAsync();
        return Ok(layers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Layer>> GetLayer(string id)
    {
        var layer = await _repository.GetByIdAsync(id);
        if (layer == null)
        {
            return NotFound();
        }
        return Ok(layer);
    }


    [HttpPost]
    public async Task<ActionResult<Layer>> CreateLayer(Layer newLayer)
    {
         await _repository.AddAsync(newLayer);
        return CreatedAtAction(nameof(GetLayer), new { id = newLayer.id }, newLayer);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLayer(string id, Layer updatedLayer)
    {
        var existingLayer = await _repository.GetByIdAsync(id);
        if (existingLayer == null)
        {
            return NotFound();
        }
        existingLayer.id = updatedLayer.id;
        existingLayer.name = updatedLayer.name;
        existingLayer.serviceUrl = updatedLayer.serviceUrl;
        existingLayer.serviceTypeId = updatedLayer.serviceTypeId;
        existingLayer.parameters = updatedLayer.parameters;
        await _repository.UpdateAsync(existingLayer);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLayer(string id)
    {
        var deleteLayer = await _repository.GetByIdAsync(id);
        if (deleteLayer == null)
        {
            return NotFound();
        }
        await _repository.DeleteAsync(id);
        return NoContent();
    }
}