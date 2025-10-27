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
public class EventController : ControllerBase
{

    private static List<Evento> events = new List<Evento>();
    private readonly IEventRepository _repository;

    /*
    private readonly DataCollectionExample _dataCollection;
    private readonly ILogger<EventController> _logger;

    public EventController(ILogger<EventController> logger, DataCollectionExample dataCollection)
    {
        _logger = logger;
        _dataCollection = dataCollection;
    }
    */

    public EventController(IEventRepository repository) {
        _repository = repository;
    }


    [HttpGet]
    public async Task<ActionResult<List<Evento>>> GetEvent()
    {
        var events = await _repository.GetAllAsync();
        return Ok(events);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Evento>> GetEvent(string id)
    {
        var evento = await _repository.GetByIdAsync(id);
        if (evento == null)
        {
            return NotFound();
        }
        return Ok(evento);
    }

    [HttpPost]
    public async Task<ActionResult<Evento>> CreateEvent(Evento newEvent)
    {
        await _repository.AddAsync(newEvent);
        return CreatedAtAction(nameof(GetEvent), new { id = newEvent.id }, newEvent);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEvent(string id, Evento updatedEvent)
    {
        var existingEvent = await _repository.GetByIdAsync(id);
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

        await _repository.UpdateAsync(existingEvent);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEvent(string id)
    {
        var deleteEvent = await _repository.GetByIdAsync(id);
        if (deleteEvent == null)
        {
            return NotFound();
        }
        await _repository.DeleteAsync(id);
        return NoContent();
    } 
}