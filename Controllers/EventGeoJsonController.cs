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
   
    //private DataCollectionExample dataCollectionExample = new DataCollectionExample();

    private static List<EventGeoJson> evenstGeoJson = new List<EventGeoJson>();
    private readonly ILogger<EventGeoJson> _logger;

    public EventGeoJsonController(ILogger<EventGeoJson> logger)
    {
        _logger = logger;
    }


    [HttpGet]
    public ActionResult<IEnumerable<EventGeoJson>> GetEventGeoJson()
    {
        return Ok(evenstGeoJson);
    }

    [HttpGet("{id}")]
    public ActionResult<EventGeoJson> GetEventGeoJson(string id)
    {
        var eventGeoJson = evenstGeoJson.FirstOrDefault(evenGJ => evenGJ.id == id);
        if (eventGeoJson == null)
        {
            return NotFound();
        }
        return Ok(eventGeoJson);
    }

    
    [HttpPost]
    public ActionResult<EventGeoJson> CreateEventGeoJson(EventGeoJson newEventGJ)
    {
        evenstGeoJson.Add(newEventGJ);
        return CreatedAtAction(nameof(newEventGJ), new { id = newEventGJ }, newEventGJ);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateEvent(string id, EventGeoJson updatedEventGeoJson)
    {
        var existingEventGJ = evenstGeoJson.FirstOrDefault(evenGJ => evenGJ.id == id);
        if (existingEventGJ == null)
        {
            return NotFound();
        }
        existingEventGJ.id = updatedEventGeoJson.id;
        existingEventGJ.features = updatedEventGeoJson.features;

        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public IActionResult DeleteEventGJ(string id)
    {
        var deleteEventGJ = evenstGeoJson.FirstOrDefault(evenGJ => evenGJ.id == id);
        if (deleteEventGJ == null)
        {
            return NotFound();
        }
        evenstGeoJson.Remove(deleteEventGJ);
        return NoContent();
    }
}
