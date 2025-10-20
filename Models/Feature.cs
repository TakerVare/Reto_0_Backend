namespace Reto_0_Backend.Models;

public class Feature
{
    
    //constructor
    public Feature(List<Property> properties, Geometry geometry)
    {
        this.properties = properties;
        this.geometry = geometry;
    }

    public string? id { get; set; }
    public List<Property> properties { get; set; }
    public Geometry geometry { get; set; }

   
}

