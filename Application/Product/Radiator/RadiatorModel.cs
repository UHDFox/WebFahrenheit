namespace Application.Product.Radiator;

public class RadiatorModel : TProduct
{
    public RadiatorModel(Guid id, string name, string article, int price, double heatedValue, string material,
        string description, string? imagePath)
        : base(id, name, article, price)
    {
        Id = id;
        Name = name;
        Article = article;
        Price = price;
        HeatedValue = heatedValue;
        Material = material;
        Description = description;
        ImagePath = imagePath;
    }

    public double HeatedValue { get; set; }

    public string Material { get; set; }

    public string Description { get; set; }

    public string? ImagePath { get; set; }
}