using Application.Fireplace.Models;
using Microsoft.AspNetCore.Http;

namespace Application.Fireplace;

public interface IFireplaceService
{
    Task<IReadOnlyCollection<GetFireplaceModel>> GetListAsync(int offset, int limit);

    Task<GetFireplaceModel> GetByIdAsync(Guid id);

    Task<Guid> AddAsync(AddFireplaceModel model, IFormFile imageFile);

    Task<UpdateFireplaceModel> UpdateAsync(UpdateFireplaceModel model, IFormFile? imageFile);

    Task<bool> DeleteAsync(Guid id);
}