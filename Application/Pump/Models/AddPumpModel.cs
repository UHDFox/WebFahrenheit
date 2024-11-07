namespace Application.Pump.Models;

public sealed class AddPumpModel 
{
    public int Price { get; set; }

    public string Name { get; set; } = "";

    public string Brand { get; set; } = "";
    
    public int Pressure { get; set; }
    
    public int PowerSupply { get; set; }
}