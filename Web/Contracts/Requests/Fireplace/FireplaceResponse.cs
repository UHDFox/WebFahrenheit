namespace Web.Contracts.Requests.Fireplace;

public sealed class FireplaceResponse
{
    public FireplaceResponse(Guid id, string name, int price, int fuelUsage, int fireLevel)
    {
        Id = id;
        Name = name;
        Price = price;
        FuelUsage = fuelUsage;
        FireLevel = fireLevel;
    }
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public int Price { get; set; }

    public int FuelUsage { get; set; }

    public int FireLevel { get; set; }

    public string? ImagePath { get; set; }
}