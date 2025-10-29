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
public class LayerParametersController : ControllerBase
{
    private static List<LayerParameters> layerParameters = new List<LayerParameters>();
    private readonly ILayerParametersRepository _repository;


    public LayerParametersController(ILayerParametersRepository repository)
    {
        _repository = repository;
    }
    

    [HttpGet]
    public async Task<ActionResult<List<LayerParameters>>> GetLayerParameters()
    {
        var layerParameters = await _repository.GetAllAsync();
        return Ok(layerParameters);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LayerParameters>> GetLayerParameters(string id)
    {
        var layerParameter = await _repository.GetByIdAsync(id);
        if (layerParameter == null)
        {
            return NotFound();
        }
        return Ok(layerParameter);
    }

    
    [HttpPost]
    public async Task<ActionResult<LayerParameters>> CreateLayerParameters(LayerParameters newLayerParameters)
    {
        await _repository.AddAsync(newLayerParameters);
        return CreatedAtAction(nameof(GetLayerParameters), new { id = newLayerParameters.id }, newLayerParameters);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLayerParameters(string id, LayerParameters updatedLayerParameters)
    {
        var existingLayerParameters = await _repository.GetByIdAsync(id);
        if (existingLayerParameters == null)
        {
            return NotFound();
        }
        existingLayerParameters.id = updatedLayerParameters.id;
        existingLayerParameters.TILEMATRIXSET = updatedLayerParameters.TILEMATRIXSET;
        existingLayerParameters.FORMAT = updatedLayerParameters.FORMAT;
        await _repository.UpdateAsync(existingLayerParameters);
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLayerParameters(string id)
    {
        var deleteLayerParameters = await _repository.GetByIdAsync(id);
        if (deleteLayerParameters == null)
        {
            return NotFound();
        }
        await _repository.DeleteAsync(id);
        return NoContent();
    }
}