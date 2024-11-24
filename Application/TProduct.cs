namespace Application;

public abstract class TProduct : TObject
{
    public TProduct(Guid id, string name, string article, int price) : base(id)
    {
        Id = id;
        Name = name;
        Article = article;
        Price = price;
    }
    public string Name { get; set; }
    
    public string Article { get; set; }
    
    public int Price { get; set; }
}