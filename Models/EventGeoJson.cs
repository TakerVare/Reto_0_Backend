namespace Reto_0_Backend.Models;

public class EventGeoJson
{

    //constructors
    public EventGeoJson()
    {
    }

    public EventGeoJson(List<Feature> features)
    {
        this.features = features;
    }

    public string? id { get; set; }
  
    public List<Feature>? features { get; set; }
}