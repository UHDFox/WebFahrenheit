namespace Application.Pump;

public sealed class PumpModel : TProduct
{
    public PumpModel(Guid id, string name, string article, int price, string brand, int pressure, string description,
        int powerSupply, string? imagePath)
        : base(id, name, article, price)
    {
        Brand = brand;
        Pressure = pressure;
        Description = description;
        PowerSupply = powerSupply;
        ImagePath = imagePath;
    }

    public string Brand { get; set; } = "";

    public int Pressure { get; set; }

    public string Description { get; set; } = "";

    public int PowerSupply { get; set; }

    public string? ImagePath { get; set; }
}