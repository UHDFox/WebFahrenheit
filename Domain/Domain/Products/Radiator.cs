namespace Domain.Domain.Products;

public sealed class Radiator : Product
{
    public Radiator(string name, int price, double heatedValue, string material) 
        : base( name, price)
    {
        HeatedValue = heatedValue;
        Material = material;
    }
    
    public double HeatedValue { get; set; }
    
    public string Material { get; set; }
}