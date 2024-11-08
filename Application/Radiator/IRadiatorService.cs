using Application.Radiator.Models;
using Microsoft.AspNetCore.Http;

namespace Application.Radiator;

public interface IRadiatorService
{
    Task<IReadOnlyCollection<GetRadiatorModel>> GetListAsync(int offset, int limit);

    Task<GetRadiatorModel> GetByIdAsync(Guid id);

    Task<Guid> AddAsync(AddRadiatorModel model, IFormFile imageFile);

    Task<UpdateRadiatorModel> UpdateAsync(UpdateRadiatorModel model, IFormFile? imageFile);

    Task<bool> DeleteAsync(Guid id);
}