namespace Application.Fireplace.Models;

public sealed class AddFireplaceModel
{
    public string? Name { get; set; }
    
    public int Price { get; set; }
    
    public int FuelUsage { get; set; }
    
    public int FireLevel { get; set; }
    
    public string? ImagePath { get; set; }
}