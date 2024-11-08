namespace Domain.Domain.Products;

public sealed class WaterBoilerRecord : Product
{
    public WaterBoilerRecord(string name, int price, int heatedValue, string material, int maxTemperature) 
        : base( name, price)
    {
        HeatedValue = heatedValue;
        Material = material;
        MaxTemperature = maxTemperature;
    }
    
    public int HeatedValue { get; set; }
    
    public string Material { get; set; }
    
    public int MaxTemperature { get; set; }
}