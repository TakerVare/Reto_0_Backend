namespace Reto_0_Backend.Models;

public class Geometry
{
    //constructor vacío
    public Geometry()
    {
    }

    //constructor con parámetros
    public Geometry(string type, double[] coordinates)
    {
        this.type = type;
        this.coordinates = coordinates;
        
    }

    public string? id { get; set; }
    public string? type { get; set; }
    public double[]? coordinates { get; set; }

   
}