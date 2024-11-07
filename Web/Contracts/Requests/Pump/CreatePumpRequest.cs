using System.ComponentModel.DataAnnotations;

namespace Web.Contracts.Requests.Pump;

public sealed class CreatePumpRequest
{


    [Required] public string Name { get; set; } = "";
    
    public int Price { get; set; }
    [Required] public string Brand { get; set; } = "";
    
    public int Pressure { get; set; }
    
    public int PowerSupply { get; set; }
    
}