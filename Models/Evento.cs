namespace Reto_0_Backend.Models;

public class Evento
{

    //constructor
    public Evento() { }
    
    public string id { get; set; }

    public string title { get; set; }
    public string? description { get; set; }
    
    public string link { get; set; }

    public string? closed { get; set; }

    public List<Category> categories { get; set; }

    public List<Source> sources { get; set; }

    public List<Geometry> geometry { get; set; }

   
}
