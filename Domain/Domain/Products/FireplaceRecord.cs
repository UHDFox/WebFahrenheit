namespace Domain.Domain.Products;

public sealed class FireplaceRecord : Product
{
    public FireplaceRecord(string name, int price, int fuelUsage, int fireLevel) 
        : base(name, price)
    {
        FuelUsage = fuelUsage;
        FireLevel = fireLevel;
    }
    
    public int FuelUsage { get; set; }
    
    public int FireLevel { get; set; }
}