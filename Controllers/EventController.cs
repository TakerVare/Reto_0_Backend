using Microsoft.AspNetCore.Mvc;
using Reto_0_Backend.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;


namespace Reto_0_Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class EventController : ControllerBase
{
   
    //private DataCollectionExample dataCollectionExample = new DataCollectionExample();

    private static List<Event> events = new List<Event>();
    private readonly ILogger<EventController> _logger;

    public EventController(ILogger<EventController> logger)
    {
        _logger = logger;
    }


    [HttpGet]
    public ActionResult<IEnumerable<Event>> GetEvent()
    {
        return Ok(events);
    }

    [HttpGet("{id}")]
    public ActionResult<Event> GetEvent(string id)
    {
        var Event = events.FirstOrDefault(even => even.id == id);
        if (Event == null)
        {
            return NotFound();
        }
        return Ok(Event);
    }

    
    [HttpPost]
    public ActionResult<Event> CreateEvent(Event newEvent)
    {
        events.Add(newEvent);
        return CreatedAtAction(nameof(GetEvent), new { id = newEvent.id }, newEvent);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateEvent(string id, Event updatedEvent)
    {
        var existingEvent = events.FirstOrDefault(even => even.id == id);
        if (existingEvent == null)
        {
            return NotFound();
        }
        existingEvent.id = updatedEvent.id;
        existingEvent.title = updatedEvent.title;
        existingEvent.description = updatedEvent.description;
        existingEvent.link = updatedEvent.link;
        existingEvent.closed = updatedEvent.closed;

        existingEvent.categories = updatedEvent.categories;
        existingEvent.sources = updatedEvent.sources;
        existingEvent.categories = updatedEvent.categories;
        existingEvent.geometry = updatedEvent.geometry;

        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public IActionResult DeleteEvent(string id)
    {
        var deleteEvent = events.FirstOrDefault(even => even.id == id);
        if (deleteEvent == null)
        {
            return NotFound();
        }
        events.Remove(deleteEvent);
        return NoContent();
    }
}
