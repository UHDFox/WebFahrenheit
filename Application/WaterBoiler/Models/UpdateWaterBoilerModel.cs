namespace Application.WaterBoiler.Models;

public sealed class UpdateWaterBoilerModel
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string Article { get; set; } = "";

    public int Price { get; set; }

    public int HeatedValue { get; set; }
    
    public string? Material { get; set; }

    public string Description { get; set; } = "";

    public int MaxTemperature { get; set; }
}