namespace Application.Fireplace.Models;

public sealed class GetFireplaceModel
{
    public GetFireplaceModel(Guid id, string name, int price, int fuelUsage, int fireLevel, string? imagePath)
    {
        Id = id;
        Name = name;
        Price = price;
        FuelUsage = fuelUsage;
        FireLevel = fireLevel;
        ImagePath = imagePath;
    }
    
    public Guid Id { get; set; }
    
    public string? Name { get; set; }
    
    public int Price { get; set; }
    
    public int FuelUsage { get; set; }
    
    public int FireLevel { get; set; }
    
    public string? ImagePath { get; set; }
}