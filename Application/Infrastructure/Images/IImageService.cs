
using Microsoft.AspNetCore.Http;

namespace Application.Infrastructure.Images;

public interface IImageService
{
    public Task<string> SaveImageLocallyAsync(IFormFile imageFile, string bucket);
}