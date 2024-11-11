namespace Web.Contracts.Requests.Fireplace;

public sealed class FireplaceResponse
{
    public FireplaceResponse(Guid id, string name, string article, int price, int fuelUsage, int fireLevel, string description, string imagePath)
    {
        Id = id;
        Name = name;
        Article = article;
        Price = price;
        FuelUsage = fuelUsage;
        FireLevel = fireLevel;
        Description = description;
        ImagePath = imagePath;
    }
    public Guid Id { get; set; }
    
    public string Name { get; set; }

    public string Article { get; set; }

    
    public int Price { get; set; }

    public int FuelUsage { get; set; }

    public int FireLevel { get; set; }
    
    public string Description { get; set; }

    public string? ImagePath { get; set; }
}