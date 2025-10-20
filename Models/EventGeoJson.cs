namespace Reto_0_Backend.Models;

public class EventGeoJson
{

    //constructor

    public EventGeoJson(List<Feature> features)
    {
        this.features = features;
    }

    public string? id { get; set; }
  
    public List<Feature> features { get; set; }
}


