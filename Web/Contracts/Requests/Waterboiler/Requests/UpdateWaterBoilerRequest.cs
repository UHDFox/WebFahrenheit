namespace Web.Contracts.Requests.Waterboiler.Requests;

public sealed class UpdateWaterBoilerRequest
{
    public Guid Id { get; set; }

    public string Name { get; set; } = "";

    public string Article { get; set; } = "";

    public int Price { get; set; }

    public int HeatedValue { get; set; }

    public string Material { get; set; } = "";

    public int MaxTemperature { get; set; }

    public string Description { get; set; } = "";
}