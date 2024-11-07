namespace Domain.Domain.Products;

public sealed class RadiatorRecord : Product
{
    public RadiatorRecord(string name, int price, double heatedValue, string material) 
        : base( name, price)
    {
        HeatedValue = heatedValue;
        Material = material;
    }
    
    public double HeatedValue { get; set; }
    
    public string Material { get; set; }
}