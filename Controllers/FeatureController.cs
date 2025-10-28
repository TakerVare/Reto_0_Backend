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
public class FeatureController : ControllerBase
{

    private static List<Feature> features = new List<Feature>();

    private readonly IFeatureRepository _repository;

     public FeatureController(IFeatureRepository repository)
    {
        _repository = repository;
    }


    [HttpGet]
    public async Task<ActionResult<List<Feature>>> GetFeature()
    {
        var features = await _repository.GetAllAsync();
        return Ok(features);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Feature>> GetFeature(string id)
    {
        var feature = await _repository.GetByIdAsync(id);
        if (feature == null)
        {
            return NotFound();
        }
        return Ok(feature);
    }

    
    [HttpPost]
    public async Task<ActionResult<Feature>> CreateFeature(Feature newFeature)
    {
        await _repository.AddAsync(newFeature);
        return CreatedAtAction(nameof(GetFeature), new { id = newFeature.id }, newFeature);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateFeature(string id, Feature updatedFeature)
    {
        var existingFeature = await _repository.GetByIdAsync(id);
        if (existingFeature == null)
        {
            return NotFound();
        }
        existingFeature.id = updatedFeature.id;
        existingFeature.properties = updatedFeature.properties;
        existingFeature.geometry = updatedFeature.geometry;

        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFeature(string id)
    {
        var deleteFeature = await _repository.GetByIdAsync(id);
        if (deleteFeature == null)
        {
            return NotFound();
        }
        await _repository.DeleteAsync(id);
        return NoContent();
    }
}