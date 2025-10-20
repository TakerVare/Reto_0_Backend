using Microsoft.AspNetCore.Mvc;
using Reto_0_Backend.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;


namespace Reto_0_Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class FeatureController : ControllerBase
{
   
    //private DataCollectionExample dataCollectionExample = new DataCollectionExample();

    private static List<Feature> features = new List<Feature>();
    private readonly ILogger<Feature> _logger;

    public FeatureController(ILogger<Feature> logger)
    {
        _logger = logger;
    }


    [HttpGet]
    public ActionResult<IEnumerable<Feature>> GetFeature()
    {
        return Ok(features);
    }

    [HttpGet("{id}")]
    public ActionResult<Feature> GetFeature(string id)
    {
        var feature = features.FirstOrDefault(f => f.id == id);
        if (feature == null)
        {
            return NotFound();
        }
        return Ok(feature);
    }

    
    [HttpPost]
    public ActionResult<Feature> CreateEventGeoJson(Feature newFeature)
    {
        features.Add(newFeature);
        return CreatedAtAction(nameof(newFeature), new { id = newFeature }, newFeature);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateFeature(string id, Feature updatedFeature)
    {
        var existingFeature = features.FirstOrDefault(f => f.id == id);
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
    public IActionResult DeleteFeature(string id)
    {
        var deleteFeature = features.FirstOrDefault(f => f.id == id);
        if (deleteFeature == null)
        {
            return NotFound();
        }
        features.Remove(deleteFeature);
        return NoContent();
    }
}
