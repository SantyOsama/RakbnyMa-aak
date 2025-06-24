namespace RakbnyMa_aak.Services
{
    public interface ICloudinaryService
    {
        Task<string> UploadImageAsync(IFormFile file,string folder);

    }
}
