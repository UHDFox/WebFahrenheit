namespace Domain.Domain.Products;

public sealed class Pump : Product
{
    public Pump(string name, int price, string brand, int pressure, int powerSupply, string? imagePath) 
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
    
    public string? ImagePath { get; set; }
}