namespace Application.WaterBoiler.Models;

public sealed class AddWaterBoilerModel
{
    public string? Name { get; set; }
    
    public int Price { get; set; }
    
    public int HeatedValue { get; set; }
    
    public string? Material { get; set; }
    
    public int MaxTemperature { get; set; }
    
    public string? ImagePath { get; set; }
}