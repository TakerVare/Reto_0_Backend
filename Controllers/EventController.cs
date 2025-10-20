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
    private readonly DataCollectionExample _dataCollection;
    private readonly ILogger<EventController> _logger;

    public EventController(ILogger<EventController> logger, DataCollectionExample dataCollection)
    {
        _logger = logger;
        _dataCollection = dataCollection;
    }


    [HttpGet]
    public ActionResult<IEnumerable<Event>> GetEvent()
    {
        return Ok(_dataCollection.EventCollectionList);
    }

    [HttpGet("{id}")]
    public ActionResult<Event> GetEvent(string id)
    {
        var Event = _dataCollection.EventCollectionList.FirstOrDefault(even => even.id == id);
        if (Event == null)
        {
            return NotFound();
        }
        return Ok(Event);
    }

    
    [HttpPost]
    public ActionResult<Event> CreateEvent(Event newEvent)
    {
        _dataCollection.EventCollectionList.Add(newEvent);
        return CreatedAtAction(nameof(GetEvent), new { id = newEvent.id }, newEvent);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateEvent(string id, Event updatedEvent)
    {
        var existingEvent = _dataCollection.EventCollectionList.FirstOrDefault(even => even.id == id);
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
        existingEvent.geometry = updatedEvent.geometry;

        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public IActionResult DeleteEvent(string id)
    {
        var deleteEvent = _dataCollection.EventCollectionList.FirstOrDefault(even => even.id == id);
        if (deleteEvent == null)
        {
            return NotFound();
        }
        _dataCollection.EventCollectionList.Remove(deleteEvent);
        return NoContent();
    }
}