namespace Domain.Domain.Products;

public sealed class RadiatorRecord : Product
{
    public RadiatorRecord(string name, string article, int price, double heatedValue, string material, string description) 
        : base(name, article, price, description)
    {
        HeatedValue = heatedValue;
        Material = material;
    }
    
    public double HeatedValue { get; set; }
    
    public string Material { get; set; }
}