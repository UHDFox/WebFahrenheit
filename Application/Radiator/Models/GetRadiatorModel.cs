namespace Application.Radiator.Models;

public sealed class GetRadiatorModel
{
    public GetRadiatorModel(Guid id, string name, int price, double heatedValue, string material, string? imagePath)
    {
        Id = id;
        Name = name;
        Price = price;
        HeatedValue = heatedValue;
        Material = material;
        ImagePath = imagePath;
    }
    public Guid Id { get; set; }
    
    public string? Name { get; set; }
    
    public int Price { get; set; }
    
    public double HeatedValue { get; set; }
    
    public string Material { get; set; }
    
    public string? ImagePath { get; set; }
}