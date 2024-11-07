namespace Web.Contracts.Requests.Waterboiler;

public sealed class CreateWaterBoilerRequest
{
    public CreateWaterBoilerRequest(string name, int price, int heatedValue, string material, int maxTemperature)
    {
        Name = name;
        Price = price;
        HeatedValue = heatedValue;
        Material = material;
        MaxTemperature = maxTemperature;
    }
    
    public string Name { get; set; }
    
    public int Price { get; set; }
    
    public int HeatedValue { get; set; }
    
    public string Material { get; set; }
    
    public int MaxTemperature { get; set; }
}