namespace Application.WaterBoiler.Models;

public class GetWaterBoilerModel
{
    public GetWaterBoilerModel(Guid id, string name, string article, int price, int heatedValue, string material, int maxTemperature, string description, string? imagePath)
    {
        Id = id;
        Name = name;
        Article = article;
        Price = price;
        HeatedValue = heatedValue;
        Material = material;
        MaxTemperature = maxTemperature;
        Description = description;
        ImagePath = imagePath;
    }
    
    public Guid Id { get; set; }
    public string Name { get; set; }

    public string Article { get; set; }

    public int Price { get; set; }
    public int HeatedValue { get; set; }
    
    public string Material { get; set; }
    
    public int MaxTemperature { get; set; }

    public string Description { get; set; } = "";
    public string? ImagePath { get; set; }
}