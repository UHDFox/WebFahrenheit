namespace Web.Contracts.Requests.Waterboiler;

public sealed class WaterBoilerResponse
{
    public WaterBoilerResponse(Guid id, string name, int price, double heatedValue, string material, string description, string? imagePath)
    {
        Id = id;
        Name = name;
        Price = price;
        Material = material;
        Description = description;
        ImagePath = imagePath;
    }
    
    public Guid Id { get; set; }
    
    public string? Name { get; set; }
    
    public int Price { get; set; }
    
    public int HeatedValue { get; set; }
    
    public string? Material { get; set; }
    
    public int MaxTemperature { get; set; }

    public string Description { get; set; } 
    
    public string? ImagePath { get; set; }
}