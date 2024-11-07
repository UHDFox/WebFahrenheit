namespace Web.Contracts.Requests.Fireplace;

public class CreateFireplaceRequest
{
    public CreateFireplaceRequest(string name, int price, int fuelUsage, int fireLevel)
    {
        Name = name;
        Price = price;
        FuelUsage = fuelUsage;
        FireLevel = fireLevel;
    }
    public string Name { get; set; }
    
    public int Price { get; set; }
    
    public int FuelUsage { get; set; }
    
    public int FireLevel { get; set; }
}