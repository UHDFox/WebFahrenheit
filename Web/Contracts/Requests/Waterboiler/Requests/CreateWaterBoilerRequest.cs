namespace Web.Contracts.Requests.Waterboiler;

public sealed class CreateWaterBoilerRequest
{
    public string? Name { get; set; }
    
    public int Price { get; set; }
    
    public int HeatedValue { get; set; }
    
    public string? Material { get; set; }
    
    public int MaxTemperature { get; set; }
}