namespace Web.Contracts.Requests.Fireplace;

public sealed class CreateFireplaceRequest
{
    public string? Name { get; set; }
    
    public int Price { get; set; }
    
    public int FuelUsage { get; set; }
    
    public int FireLevel { get; set; }
}