namespace Web.Contracts.Requests.Radiator;

public class RadiatorResponse
{
    public RadiatorResponse(Guid id, string name, int price, double heatedValue, string material)
    {
        Id = id;
        Name = name;
        Price = price;
        HeatedValue = heatedValue;
        Material = material;
    }
    public Guid Id { get; set; }
    public string Name { get; set; }
    
    public int Price { get; set; }
    
    public double HeatedValue { get; set; }
    
    public string Material { get; set; }
}