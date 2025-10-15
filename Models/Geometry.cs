namespace Reto_0_Backend.Models;

public class Geometry
{


    public string type { get; set; }
    public double[] coordinates { get; set; }
    
    //constructor
    public Geometry(string type, double[] coordinates)
    {
        this.type = type;
        this.coordinates[0] = coordinates[0];
        this.coordinates[1] = coordinates[1];

    }

   

   
}

