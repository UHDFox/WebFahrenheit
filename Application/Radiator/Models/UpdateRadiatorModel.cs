namespace Application.Radiator.Models;

public sealed class UpdateRadiatorModel
{
    public Guid Id { get; set; }
    
    public string? Name { get; set; }
    
    public int Price { get; set; }
    
    public double HeatedValue { get; set; }
    
    public string? Material { get; set; }

    public string Description { get; set; } = "";
}