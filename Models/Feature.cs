namespace Reto_0_Backend.Models;

public class Feature
{
    
    //constructors
    // Constructor vacío (necesario para inicializadores de objetos)
    public Feature()
    {
    }
    
    // Constructor con parámetros
    public Feature(List<Property> properties, Geometry geometry)
    {
        this.properties = properties;
        this.geometry = geometry;
    }

    public string? id { get; set; }
    public List<Property>? properties { get; set; }
    public Geometry? geometry { get; set; }

   
}