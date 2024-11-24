using Microsoft.AspNetCore.Http;

namespace Application.Infrastructure.Images;

public interface IImageService
{
    public Task<string> SaveImageLocallyAsync(IFormFile imageFile, string bucket);

    public Task<string> UpdateImageAsync(IFormFile newImageFile, string bucket, string? existingImagePath = null);
    public Task<bool> DeleteImageAsync(string imagePath);
}