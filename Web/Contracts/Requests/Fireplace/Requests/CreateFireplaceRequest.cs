namespace Web.Contracts.Requests.Fireplace.Requests;

public sealed class CreateFireplaceRequest
{
    public string? Name { get; set; }
    
    public int Price { get; set; }
    
    public int FuelUsage { get; set; }
    
    public int FireLevel { get; set; }

    public string Description { get; set; } = "";
}