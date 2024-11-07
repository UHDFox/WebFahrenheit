namespace Application.Pump.Models;

public sealed class UpdatePumpModel
{
    public UpdatePumpModel(Guid id, string name, int price, string brand, int pressure, int powerSupply, string? imagePath)
    {
        Id = id;
        Name = name;
        Price = price;
        Brand = brand;
        Pressure = pressure;
        PowerSupply = powerSupply;
        ImagePath = imagePath;
    }
    public Guid Id { get; set; }
    
    public int Price { get; set; }
    
    public string Name { get; set; }
    
    public string Brand { get; set; }
    
    public int Pressure { get; set; }
    
    public int PowerSupply { get; set; }

    private string? ImagePath { get; set; }
}