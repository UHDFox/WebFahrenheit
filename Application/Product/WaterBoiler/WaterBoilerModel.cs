namespace Application.Product.WaterBoiler;

public class WaterBoilerModel : TProduct
{
    public WaterBoilerModel(Guid id, string name, string article, int price, int heatedValue, string material,
        int maxTemperature, string description, string? imagePath)
        : base(id, name, article, price)
    {
        HeatedValue = heatedValue;
        Material = material;
        MaxTemperature = maxTemperature;
        Description = description;
        ImagePath = imagePath;
    }

    public int HeatedValue { get; set; }

    public string Material { get; set; }

    public int MaxTemperature { get; set; }

    public string Description { get; set; } = "";

    public string? ImagePath { get; set; }
}