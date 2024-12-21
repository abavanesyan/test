using Azure.Storage.Blobs;
using System.IO;
using System.Threading.Tasks;
using UFAR.Classroom.Services;

namespace UFAR.Classroom.Services
{
    public class SubmissionService : ISubmissionService
    {
        // Store the connection string securely, e.g., in appsettings.json or as an environment variable
        private readonly string _connectionString = "DefaultEndpointsProtocol=https;AccountName=virtulearn01;AccountKey=ksk+9NKKwHBtBbQlwkXyHz8u1/Fo1nIPfwJuYrl/bGCs/PDy3Wk21Kco1K6mrDpT56mp4ONvUdAG+ASt87dIIg==;EndpointSuffix=core.windows.net";

        // Define your container name here
        private readonly string _containerName = "virtulearn01";

        public async Task<string> UploadFileToBlobAsync(Stream fileStream, string fileName)
        {
            // Create a BlobServiceClient
            BlobServiceClient blobServiceClient = new BlobServiceClient(_connectionString);

            // Get the container client
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(_containerName);

            // Create the container if it doesn't exist
            await containerClient.CreateIfNotExistsAsync();

            // Get the blob client
            BlobClient blobClient = containerClient.GetBlobClient(fileName);

            // Upload the file
            await blobClient.UploadAsync(fileStream, overwrite: true);

            // Return the blob's URI
            return blobClient.Uri.ToString();
        }
        public async Task DeleteFileFromBlobAsync(string fileUrl)
        {
            // Create a BlobServiceClient
            BlobServiceClient blobServiceClient = new BlobServiceClient(_connectionString);

            // Extract the blob name from the file URL
            Uri fileUri = new Uri(fileUrl);
            string blobName = Path.GetFileName(fileUri.LocalPath);

            // Get the container client
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(_containerName);

            // Get the blob client
            BlobClient blobClient = containerClient.GetBlobClient(blobName);

            // Delete the blob
            await blobClient.DeleteIfExistsAsync();
        }



    }
}