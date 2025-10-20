namespace Reto_0_Backend.Models;

public class Property
{

    //constructors
    // Constructor vacío (por compatibilidad con serialización o frameworks)
    public Property()
    {
    }

    // Constructor con parámetros
    public Property(
        string id,
        string title,
        string description,
        string link,
        string closed,
        DateTime date,
        double magnitudeValue,
        string magnitudeUnit,
        List<Category> categories,
        List<Source> sources)
    {
        this.id = id;
        this.title = title;
        this.description = description;
        this.link = link;
        this.closed = closed;
        this.date = date;
        this.magnitudeValue = magnitudeValue;
        this.magnitudeUnit = magnitudeUnit;
        this.categories = categories;
        this.sources = sources;
    }

    public string? id { get; set; }

    public string? title { get; set; }

    public string? description { get; set; }

    public string? link { get; set; }

    public string? closed { get; set; }

    public DateTime date { get; set; }

    public double magnitudeValue { get; set; }

    public string? magnitudeUnit { get; set; }

    public List<Category>? categories { get; set; }
    
    public List<Source>? sources { get; set; }
    
   
}