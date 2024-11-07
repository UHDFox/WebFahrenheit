namespace Domain.Domain.Products;

public class PumpRecord : Product
{
    public PumpRecord(string name, int price, string brand, int pressure, int powerSupply, string? imagePath) 
        : base(name, price)
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