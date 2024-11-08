namespace Application.Pump.Models;

public sealed class UpdatePumpModel
{
    public Guid Id { get; set; }
    
    public int Price { get; set; }
    
    public string? Name { get; set; }
    
    public string? Brand { get; set; }
    
    public int Pressure { get; set; }
    
    public int PowerSupply { get; set; }

    private string? ImagePath { get; set; }
}