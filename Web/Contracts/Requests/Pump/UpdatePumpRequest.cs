namespace Web.Contracts.Requests.Pump;

public class UpdatePumpRequest
{
    public UpdatePumpRequest(Guid id, string name, int price, string brand, int pressure, int powerSupply)
    {
        Id = id;
        Name = name;
        Price = price;
        Brand = brand;
        Pressure = pressure;
        PowerSupply = powerSupply;
    }
    
    public Guid Id { get; set; }

    public string Name { get; set; }
    
    public int Price { get; set; }
    
    public string Brand { get; set; }
    
    public int Pressure { get; set; }
    
    public int PowerSupply { get; set; }
}