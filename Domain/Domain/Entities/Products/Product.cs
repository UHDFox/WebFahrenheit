namespace Domain.Domain.Entities.Products;

public abstract class Product : IDataObject
{
    public Product(string name, string article, int price, string description)
    {
        Name = name;
        Article = article;
        Price = price;
        Description = description;
    }
    public string Article { get; set; }
    
    public string Name { get; set; }
    
    public int Price { get; set; }

    public string Description { get; set; }
    public string? ImagePath { get; set; }
}