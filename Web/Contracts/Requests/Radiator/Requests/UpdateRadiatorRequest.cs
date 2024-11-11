namespace Web.Contracts.Requests.Radiator.Requests;

public sealed class UpdateRadiatorRequest
{
    public Guid Id { get; set; }
    
    public string? Name { get; set; }

    public string Article { get; set; } = "";
    
    public int Price { get; set; }
    
    public double HeatedValue { get; set; }
    
    public string? Material { get; set; }

    public string Description { get; set; } = "";
}