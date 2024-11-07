namespace Web.Contracts.Requests.Waterboiler;

public class UpdateWaterBoilerRequest
{
    public UpdateWaterBoilerRequest(Guid id, string name, int price, int heatedValue, string material, int maxTemperature)
    {
        Id = id;
        Name = name;
        Price = price;
        HeatedValue = heatedValue;
        Material = material;
        MaxTemperature = maxTemperature;
    }
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public int Price { get; set; }
    
    public int HeatedValue { get; set; }
    
    public string Material { get; set; }
    
    public int MaxTemperature { get; set; }
}