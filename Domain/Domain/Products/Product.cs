namespace Domain.Domain.Products;

public abstract class Product
{
    public Product(string name, int price)
    {
        Name = name;
        Price = price;
    }
    
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public int Price { get; set; }
    
    public int Width { get; set; }
    
    public int Height { get; set; }
}