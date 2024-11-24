using Domain.Domain.Entities.Products;

namespace Domain.Domain;

public sealed class ProductImage
{
    public ProductImage(string imagePath, Guid productId)
    {
        ImagePath = imagePath;
        ProductId = productId;
    }

    public Guid Id { get; set; }

    public string ImagePath { get; set; }

    public Guid ProductId { get; set; }

    public Product? Product { get; set; }
}