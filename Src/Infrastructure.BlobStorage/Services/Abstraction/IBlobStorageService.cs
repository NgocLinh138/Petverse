using Microsoft.AspNetCore.Http;

namespace Infrastructure.BlobStorage.Services.Abstraction;
public interface IBlobStorageService
{
    Task<string> UploadBlob(IFormFile image, string name, string folder);
    Task<List<string>> UploadFormMultiBlobs(List<IFormFile> listImages, string name, string folder, string? containerName = null);
    Task DeleteBlobAsync(string urlString, string? containerName = null);
    Task DeleteMultiBlobAsync(List<string> urlString);
    Task DeleteBlobSnapshotsAsync(string urlString, string? containerName = null);
    Task RestoreBlobsAsync(string urlString, string? containerName = null);
    Task RestoreSnapshotsAsync(string urlString, string? containerName = null);
    bool IsImageOrVideo(IFormFile file);
    bool IsImage(IFormFile file);
    bool IsVideo(IFormFile file);
}
