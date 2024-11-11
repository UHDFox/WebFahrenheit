namespace Domain.Domain.Products;

public sealed class FireplaceRecord : Product
{
    public FireplaceRecord(string name, string article, int price, int fuelUsage, int fireLevel, string description) 
        : base(name, article, price, description)
    {
        FuelUsage = fuelUsage;
        FireLevel = fireLevel;
    }
    
    public int FuelUsage { get; set; }
    
    public int FireLevel { get; set; }
}