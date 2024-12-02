using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using Infrastructure.BlobStorage.DependencyInjection.Options;
using Infrastructure.BlobStorage.Services.Abstraction;
using MediaToolkit;
using MediaToolkit.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Infrastructure.BlobStorage.Services;
public class BlobStorageService : IBlobStorageService
{
    private readonly BlobStorageOptions blobStorageOptions;
    private const double MAXDURATION = 10;
    public BlobStorageService(IOptions<BlobStorageOptions> blobStorageOptions)
    {
        this.blobStorageOptions = blobStorageOptions.Value;
    }

    public async Task<string> UploadBlob(IFormFile file, string name, string folder)
    {
        // Create Path
        string blobPath = $"{folder}/{name}-{DateTimeOffset.Now.ToUnixTimeSeconds()}";
        BlobClient blobClient = GetBlobClient(blobPath);

        // Open the file and upload its data
        using Stream stream = file.OpenReadStream();
        await blobClient.UploadAsync(stream);
        stream.Close();
        return blobClient.Uri.AbsoluteUri;
    }
    public async Task<List<string>> UploadFormMultiBlobs(List<IFormFile> listFile, string name, string folder, string? containerName = null)
    {
        List<string> urls = new List<string>();
        BlobContainerClient container = GetBlobContainerClient(containerName);

        foreach (IFormFile file in listFile)
        {
            string path = $"{folder}/{name}-{new Random().Next(0, int.MaxValue)}-{DateTimeOffset.Now.ToUnixTimeSeconds()}";
            BlobClient blobClient = container.GetBlobClient(path);

            // Open the file and upload its data
            using Stream stream = file.OpenReadStream();
            await blobClient.UploadAsync(stream);
            urls.Add(blobClient.Uri.AbsoluteUri);
            stream.Close();
        }

        return urls;
    }

    public async Task DeleteBlobAsync(string urlString, string? containerName = null)
    {
        BlobClient blobClient = GetBlobClient(GetBlobPath(urlString), containerName);

        // Check if the blob exists before attempting to delete
        if (await blobClient.ExistsAsync())
            await blobClient.DeleteAsync();
    }

    public async Task DeleteBlobSnapshotsAsync(string urlString, string? containerName = null)
    {
        BlobClient blobClient = GetBlobClient(GetBlobPath(urlString), containerName);

        // Check if the blob exists before attempting to delete
        if (await blobClient.ExistsAsync())
        {
            // Delete a blob and all of its snapshots
            await blobClient.DeleteAsync(snapshotsOption: DeleteSnapshotsOption.IncludeSnapshots);

            // Delete only the blob's snapshots
            //await blob.DeleteAsync(snapshotsOption: DeleteSnapshotsOption.OnlySnapshots);
        }
    }
    public async Task DeleteMultiBlobAsync(List<string> urls)
    {
        foreach (string urlString in urls)
        {
            await DeleteBlobAsync(urlString);
        }
    }

    public async Task RestoreBlobsAsync(string urlString, string? containerName = null)
    {
        var blobClient = GetBlobClient(GetBlobPath(urlString), containerName);
        // Restore the deleted blob
        await blobClient.UndeleteAsync();
    }
    public async Task RestoreSnapshotsAsync(string urlString, string? containerName = null)
    {
        var blobClient = GetBlobClient(GetBlobPath(urlString), containerName);

        // Restore the deleted blob
        await blobClient.UndeleteAsync();

        // Restore the most recent snapshot by copying it to the blob
        var blobUri = GetMostRecentSnapshotUri(blobClient);
        await blobClient.StartCopyFromUriAsync(blobUri);
    }

    private Uri GetMostRecentSnapshotUri(BlobClient blobClient)
    {
        // List blobs in this container that match prefix
        // Include snapshots in listing
        var blobItems = blobClient.GetParentBlobContainerClient().GetBlobs(
            BlobTraits.None, BlobStates.Snapshots, prefix: blobClient.Name);

        // Get the URI for the most recent snapshot
        var mostRecentSnapshot = blobItems.OrderByDescending(s => s.Snapshot)
            .FirstOrDefault()?.Snapshot;
        var blobUri = new BlobUriBuilder(blobClient.Uri)
        {
            Snapshot = mostRecentSnapshot
        }.ToUri(); ;


        return blobUri;
    }
    private BlobClient GetBlobClient(string blobPath, string? containerName = null)
    {
        var container = GetBlobContainerClient(containerName);
        return container.GetBlobClient(blobPath);
    }
    private BlobContainerClient GetBlobContainerClient(string? containerName = null)
    {
        var blobServiceClient = new BlobServiceClient(blobStorageOptions.ConnectionString);
        return blobServiceClient.GetBlobContainerClient(containerName ?? blobStorageOptions.Container);
    }
    private string GetBlobPath(string uriString)
    {
        Uri uri = new Uri(uriString);
        string containerName = uri.Segments[1];  // Assuming the container name is the second segment in the URI

        // Get blobPath
        int startIndex = containerName.Length + 1;
        int length = uri.PathAndQuery.Length - containerName.Length - 1;
        string blobPath = uri.PathAndQuery.Substring(startIndex, length);
        return blobPath;
    }
    private BlobClient GetBlobClient(string blobPath)
    {
        var blobServiceClient = new BlobServiceClient(blobStorageOptions.ConnectionString);
        var blobContainerClient = blobServiceClient.GetBlobContainerClient(blobStorageOptions.Container);
        return blobContainerClient.GetBlobClient(blobPath);
    }
    private async Task<bool> BlobExists(BlobClient blobClient)
    {
        try
        {
            return await blobClient.ExistsAsync();
        }
        catch
        {
            return false;
        }
    }

    public bool IsImageOrVideo(IFormFile file)
    {
        return IsImage(file) || IsVideo(file);
    }
    public bool IsImage(IFormFile file)
    {
        if (file == null)
            return false;

        string[] allowedImageTypes =
        {
            "image/jpeg", "image/png", "image/gif", "image/bmp", "image/webp",
            "image/svg+xml", "image/tiff", "image/x-icon"
        };
        return allowedImageTypes.Contains(file.ContentType);
    }

    public bool IsVideo(IFormFile file)
    {
        if (file == null)
            return false;

        string[] allowedVideoTypes =
        {
            "video/mp4", "video/webm", "video/ogg", "video/avi", "video/mpeg",
            "video/quicktime", "video/x-msvideo", "video/x-matroska"
        };
        return allowedVideoTypes.Contains(file.ContentType);//&& CheckDuration(file)
    }

    private bool CheckDuration(IFormFile file)
    {
        var filePath = Path.GetTempFileName();
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            file.CopyTo(stream);
        }
        var inputFile = new MediaFile { Filename = filePath };
        using (var engine = new Engine())
        {
            engine.GetMetadata(inputFile);
        }
        var duration = inputFile.Metadata.Duration.TotalSeconds;
        return duration < MAXDURATION;
    }

}
