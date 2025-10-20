using Microsoft.AspNetCore.Mvc;
using Reto_0_Backend.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;


namespace Reto_0_Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class EventGeoJsonController : ControllerBase
{
    private readonly DataCollectionExample _dataCollection;
    private readonly ILogger<EventGeoJson> _logger;

    public EventGeoJsonController(ILogger<EventGeoJson> logger, DataCollectionExample dataCollection)
    {
        _logger = logger;
        _dataCollection = dataCollection;
    }


    [HttpGet]
    public ActionResult<IEnumerable<EventGeoJson>> GetEventGeoJson()
    {
        // Crear EventGeoJson a partir de FeatureCollectionList
        var eventGeoJson = new EventGeoJson
        {
            id = "geojson-collection",
            features = _dataCollection.FeatureCollectionList
        };
        return Ok(new List<EventGeoJson> { eventGeoJson });
    }

    [HttpGet("{id}")]
    public ActionResult<EventGeoJson> GetEventGeoJson(string id)
    {
        // Crear EventGeoJson dinámicamente
        var eventGeoJson = new EventGeoJson
        {
            id = id,
            features = _dataCollection.FeatureCollectionList
        };
        return Ok(eventGeoJson);
    }

    
    [HttpPost]
    public ActionResult<EventGeoJson> CreateEventGeoJson(EventGeoJson newEventGJ)
    {
        if (newEventGJ.features != null)
        {
            _dataCollection.FeatureCollectionList.AddRange(newEventGJ.features);
        }
        return CreatedAtAction(nameof(GetEventGeoJson), new { id = newEventGJ.id }, newEventGJ);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateEvent(string id, EventGeoJson updatedEventGeoJson)
    {
        if (updatedEventGeoJson.features != null)
        {
            // Actualizar las features en la colección
            _dataCollection.FeatureCollectionList.Clear();
            _dataCollection.FeatureCollectionList.AddRange(updatedEventGeoJson.features);
        }
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public IActionResult DeleteEventGJ(string id)
    {
        // Limpiar la lista de features
        _dataCollection.FeatureCollectionList.Clear();
        return NoContent();
    }
}