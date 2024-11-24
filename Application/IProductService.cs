using Microsoft.AspNetCore.Http;

namespace Application;


public interface IProductService<TModel> : IService<TModel> where TModel : TProduct
{
    public new Task<Guid> AddAsync(TModel model, IFormFile? imageFile);

    public new Task<TModel> UpdateAsync(TModel model, IFormFile? imageFile);
}