using Application.WaterBoiler.Models;
using Microsoft.AspNetCore.Http;

namespace Application.WaterBoiler;

public interface IWaterBoilerService
{
    Task<IReadOnlyCollection<GetWaterBoilerModel>> GetListAsync(int offset, int limit);

    Task<GetWaterBoilerModel> GetByIdAsync(Guid id);

    Task<Guid> AddAsync(AddWaterBoilerModel model, IFormFile imageFile);

    Task<UpdateWaterBoilerModel> UpdateAsync(UpdateWaterBoilerModel model, IFormFile? imageFile);

    Task<bool> DeleteAsync(Guid id);
}