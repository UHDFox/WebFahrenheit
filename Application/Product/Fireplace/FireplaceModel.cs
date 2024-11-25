namespace Application.Product.Fireplace;

public sealed class FireplaceModel : TProduct
{
    public FireplaceModel(Guid id, string name, string article, int price, int fuelUsage, int fireLevel,
        string description, string? imagePath)
        : base(id, name, article, price)
    {
        Name = name;
        Article = article;
        Price = price;
        FuelUsage = fuelUsage;
        FireLevel = fireLevel;
        Description = description;
        ImagePath = imagePath;
    }

    public int FuelUsage { get; set; }

    public int FireLevel { get; set; }

    public string Description { get; set; }

    public string? ImagePath { get; set; }
}