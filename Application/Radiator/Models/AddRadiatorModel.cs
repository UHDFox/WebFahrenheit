namespace Application.Radiator.Models;

public sealed class AddRadiatorModel
{
    public string? Name { get; set; }
    
    public int Price { get; set; }
    
    public double HeatedValue { get; set; }

    public string Description { get; set; } = "";
    
    public string? Material { get; set; }
}