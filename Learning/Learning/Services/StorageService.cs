using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lms.Services
{
    public class StorageService : IStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly IConfiguration _configuration;

        public StorageService(BlobServiceClient blobServiceClient, IConfiguration configuration)
        {
            _blobServiceClient = blobServiceClient;
            _configuration = configuration;
        }
        public async Task<string> Upload(IFormFile formFile, string FileName)
        {
            var containerName = _configuration.GetSection("Storage:ContainerName").Value;

            var blobContainer = _blobServiceClient.GetBlobContainerClient(containerName);

            var blobClient = blobContainer.GetBlobClient(FileName);

            await blobClient.UploadAsync(formFile.OpenReadStream());
            var blobUrl = blobClient.Uri.AbsoluteUri;
            return blobUrl;
        }
    }
}
