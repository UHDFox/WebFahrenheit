namespace Web.Contracts.Requests.Pump;

public sealed class PumpResponse
{
    public PumpResponse(Guid id, string name, string article, int price, string brand, int pressure, int powerSupply,
        string description, string imagePath)
    {
        Id = id;
        Name = name;
        Article = article;
        Price = price;
        Brand = brand;
        Pressure = pressure;
        PowerSupply = powerSupply;
        Description = description;
        ImagePath = imagePath;
    }

    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Article { get; set; }

    public int Price { get; set; }

    public string Brand { get; set; }

    public int Pressure { get; set; }

    public int PowerSupply { get; set; }

    public string Description { get; set; }

    public string ImagePath { get; set; }
}