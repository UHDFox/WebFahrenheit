namespace Domain.Domain.Products;

public class PumpRecord : Product
{
    public PumpRecord(string name, string article, int price, string brand, int pressure, int powerSupply, string description, string? imagePath) 
        : base(name, article, price, description)
    {
        Brand = brand;
        Pressure = pressure;
        PowerSupply = powerSupply;
        ImagePath = imagePath;
    }
    public string Brand { get; set; }
    
    public int Pressure { get; set; }
    
    public int PowerSupply { get; set; }
}