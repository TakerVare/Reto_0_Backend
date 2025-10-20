using Microsoft.AspNetCore.Mvc;
using Reto_0_Backend.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;


namespace Reto_0_Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class LayerParametersController : ControllerBase
{
   
    //private DataCollectionExample dataCollectionExample = new DataCollectionExample();

    private static List<LayerParameters> layersParameters = new List<LayerParameters>();
    private readonly ILogger<LayerParameters> _logger;

    public LayerParametersController(ILogger<LayerParameters> logger)
    {
        _logger = logger;
    }


    [HttpGet]
    public ActionResult<IEnumerable<LayerParameters>> GetLayerParameters()
    {
        return Ok(layersParameters);
    }

    [HttpGet("{id}")]
    public ActionResult<LayerParameters> GetLayerParameters(string id)
    {
        var layerParameter = layersParameters.FirstOrDefault(l => l.id == id);
        if (layerParameter == null)
        {
            return NotFound();
        }
        return Ok(layerParameter);
    }

    
    [HttpPost]
    public ActionResult<LayerParameters> CreateLayerParameters(LayerParameters newLayerParameters)
    {
        layersParameters.Add(newLayerParameters);
        return CreatedAtAction(nameof(newLayerParameters), new { id = newLayerParameters }, newLayerParameters);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateLayerParameters(string id, LayerParameters updatedLayerParameters)
    {
        var existingLayerParameters = layersParameters.FirstOrDefault(l => l.id == id);
        if (existingLayerParameters == null)
        {
            return NotFound();
        }
        existingLayerParameters.id = updatedLayerParameters.id;
        existingLayerParameters.TILEMATRIXSET = updatedLayerParameters.TILEMATRIXSET;
        existingLayerParameters.FORMAT = updatedLayerParameters.FORMAT;
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public IActionResult DeleteLayerParameters(string id)
    {
        var deleteLayerParameters = layersParameters.FirstOrDefault(l => l.id == id);
        if (deleteLayerParameters == null)
        {
            return NotFound();
        }
        layersParameters.Remove(deleteLayerParameters);
        return NoContent();
    }
}
