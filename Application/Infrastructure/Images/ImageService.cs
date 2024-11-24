using Microsoft.AspNetCore.Http;

namespace Application.Infrastructure.Images;

public sealed class ImageService : IImageService
{
    private readonly string _baseUploadsFolder = "uploads";

    public async Task<string> SaveImageLocallyAsync(IFormFile imageFile, string bucket)
    {
        var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";

        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), _baseUploadsFolder, bucket);

        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }

        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        await using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await imageFile.CopyToAsync(stream);
        }

        return Path.Combine("/", _baseUploadsFolder, bucket, uniqueFileName).Replace("\\", "/");
    }


    public async Task<string> UpdateImageAsync(IFormFile newImageFile, string bucket, string? existingImagePath = null)
    {
        if (!string.IsNullOrEmpty(existingImagePath))
        {
            await DeleteImageAsync(existingImagePath);
        }

        return await SaveImageLocallyAsync(newImageFile, bucket);
    }

    public Task<bool> DeleteImageAsync(string imagePath)
    {
        try
        {
            // Combine the base directory with the relative image path
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), imagePath.TrimStart('/'));

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }
        catch (Exception)
        {
            // Log error if needed
            return Task.FromResult(false);
        }
    }
}