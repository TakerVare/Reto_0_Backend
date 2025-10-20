namespace Reto_0_Backend.Models;

public class Layer
{

    //constructor
    public Layer() { }
    
    public string? id { get; set; }
    public string? name { get; set; }

    public string? serviceUrl { get; set; }
    public string? serviceTypeId { get; set; }

    public List<LayerParameters>? parameters { get; set; }

   
}