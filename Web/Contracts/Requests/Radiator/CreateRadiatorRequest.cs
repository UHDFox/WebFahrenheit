namespace Web.Contracts.Requests.Radiator;

public class CreateRadiatorRequest
{
    public CreateRadiatorRequest(string name, int price, double heatedValue, string material)
    {
        Name = name;
        Price = price;
        HeatedValue = heatedValue;
        Material = material;
    }
    public string Name { get; set; }
    
    public int Price { get; set; }
    
    public double HeatedValue { get; set; }
    
    public string Material { get; set; }
}