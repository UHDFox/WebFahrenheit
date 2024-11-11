namespace Domain.Domain.Products;

public sealed class WaterBoilerRecord : Product
{
    public WaterBoilerRecord(string name, string article, int price, int heatedValue, string material, int maxTemperature, string description) 
        : base(name, article, price, description)
    {
        HeatedValue = heatedValue;
        Material = material;
        MaxTemperature = maxTemperature;
    }
    
    public int HeatedValue { get; set; }
    
    public string Material { get; set; }
    
    public int MaxTemperature { get; set; }
}