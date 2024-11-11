namespace Application.Fireplace.Models;

public sealed class UpdateFireplaceModel
{
    public Guid Id { get; set; }
    
    public string? Name { get; set; }

    public string Article { get; set; } = "";
    
    public int Price { get; set; }
    
    public int FuelUsage { get; set; }
    
    public int FireLevel { get; set; }

    public string Description { get; set; } = "";
}