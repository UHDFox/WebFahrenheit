namespace Domain.Domain.Products;

public sealed class Fireplace : Product
{
    public Fireplace(string name, int price, int fuelUsage, int fireLevel) 
        : base(name, price)
    {
        FuelUsage = fuelUsage;
        FireLevel = fireLevel;
    }
    
    public int FuelUsage { get; set; }
    
    public int FireLevel { get; set; }
}