using Microsoft.AspNetCore.Http;

namespace Application.Product;

public interface IProductService<TModel> : IService<TModel> where TModel : TProduct
{
    public Task<Guid> AddAsync(TModel model, IFormFile? imageFile);

    public Task<TModel> UpdateAsync(TModel model, IFormFile? imageFile);
}