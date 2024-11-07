using Application.Pump.Models;
using Microsoft.AspNetCore.Http;

namespace Application.Pump;

public interface IPumpService
{
    Task<IReadOnlyCollection<GetPumpModel>> GetAllAsync(int offset, int limit);

    Task<GetPumpModel> GetByIdAsync(Guid id);

    Task<Guid> AddAsync(AddPumpModel model, IFormFile imageFile);

    Task<UpdatePumpModel> UpdateAsync(UpdatePumpModel model, IFormFile? imageFile);

    Task<bool> DeleteAsync(Guid id);
}