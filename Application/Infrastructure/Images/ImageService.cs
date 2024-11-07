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
}

