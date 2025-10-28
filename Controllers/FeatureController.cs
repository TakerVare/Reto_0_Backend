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

    /*
    private readonly DataCollectionExample _dataCollection;
    private readonly ILogger<Feature> _logger;

    public FeatureController(ILogger<Feature> logger, DataCollectionExample dataCollection)
    {
        _logger = logger;
        _dataCollection = dataCollection;
    }
    */

    private static List<Feature> features = new List<Feature>(); = new List<Evento>();

    private readonly IFeatureRepository _repository;

    
    public FeatureRepository(IFeatureRepository repository) {
        _repository = repository;
    }


    [HttpGet]
    public ActionResult<IEnumerable<Feature>> GetFeature()
    {
        var features = await _repository.GetAllAsync();
        return Ok(features);
    }

    [HttpGet("{id}")]
    public ActionResult<Feature> GetFeature(string id)
    {
        var feature = _dataCollection.FeatureCollectionList.FirstOrDefault(f => f.id == id);
        if (feature == null)
        {
            return NotFound();
        }
        return Ok(feature);
    }

    
    [HttpPost]
    public ActionResult<Feature> CreateFeature(Feature newFeature)
    {
        _dataCollection.FeatureCollectionList.Add(newFeature);
        return CreatedAtAction(nameof(GetFeature), new { id = newFeature.id }, newFeature);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateFeature(string id, Feature updatedFeature)
    {
        var existingFeature = _dataCollection.FeatureCollectionList.FirstOrDefault(f => f.id == id);
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
        var deleteFeature = _dataCollection.FeatureCollectionList.FirstOrDefault(f => f.id == id);
        if (deleteFeature == null)
        {
            return NotFound();
        }
        _dataCollection.FeatureCollectionList.Remove(deleteFeature);
        return NoContent();
    }
}