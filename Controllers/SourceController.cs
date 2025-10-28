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
public class SourceController : ControllerBase
{
    private static List<Source> sources = new List<Source>();
    private readonly ISourceRepository _repository;

    
    public SourceController(ISourceRepository repository) {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<List<Source>>> GetSources()
    {
        var sources = await _repository.GetAllAsync();
        return Ok(sources);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Source>> GetSource(string id)
    {
        var source = await _repository.GetByIdAsync(id);
        if (source == null)
        {
            return NotFound();
        }
        return Ok(source);
    }

    
    [HttpPost]
    public async Task<ActionResult<Source>> CreateSource(Source newSource)
    {
        await _repository.AddAsync(newSource);
        return CreatedAtAction(nameof(GetSource), new { id = newSource.id }, newSource);
    }

    [HttpPut("{id}")]
     public async Task<IActionResult> UpdateSource(string id, Source updatedSource)
    {
        var existingSource = await _repository.GetByIdAsync(id);
        if (existingSource == null)
        {
            return NotFound();
        }
        existingSource.id = updatedSource.id;
        existingSource.url = updatedSource.url;

        await _repository.UpdateAsync(existingSource);
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSource(string id)
    {
        var deleteSource = await _repository.GetByIdAsync(id);
        if (deleteSource == null)
        {
            return NotFound();
        }
        await _repository.DeleteAsync(id);
        return NoContent();
    }
}