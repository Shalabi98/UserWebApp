using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserWebApp.IServices;

namespace UserWebApp.Services
{
    public class BlobService : IBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;

        public BlobService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task<Models.BlobInfo> GetBlob(string name)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient("uploadimagetoblob");
            var blobClient = containerClient.GetBlobClient(name);
            var blobDownloadInfo = await blobClient.DownloadAsync();

            return new Models.BlobInfo(blobDownloadInfo.Value.Content, blobDownloadInfo.Value.ContentType);
        }

        public async Task<IEnumerable<string>> GetAllBlobs()
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient("uploadimagetoblob");
            var items = new List<string>();
            var blobs = containerClient.GetBlobsAsync();

            await foreach (var blobItem in blobs)
            {
                items.Add(blobItem.Name);
            }

            return items;
        }

        public async Task<bool> UploadBlob(string name, IFormFile file)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient("uploadimagetoblob");
            var blobClient = containerClient.GetBlobClient(name);
            var httpHeaders = new BlobHttpHeaders() { ContentType = file.ContentType };

            var res = await blobClient.UploadAsync(file.OpenReadStream(), httpHeaders);

            if (res != null)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteBlob(string name)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient("uploadimagetoblob");
            var blobClient = containerClient.GetBlobClient(name);

            return await blobClient.DeleteIfExistsAsync();
        }
    }
}
