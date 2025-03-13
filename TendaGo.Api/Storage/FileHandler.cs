using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;

namespace TendaGo.Api.Storage
{
    /// <summary>   
    /// FileHandler for Blob Service
    /// Documentation References:  
    /// - What is a Storage Account - http://azure.microsoft.com/en-us/documentation/articles/storage-whatis-account/ 
    /// - Getting Started with Blobs - http://azure.microsoft.com/en-us/documentation/articles/storage-dotnet-how-to-use-blobs/ 
    /// - Blob Service Concepts - http://msdn.microsoft.com/en-us/library/dd179376.aspx  
    /// - Blob Service REST API - http://msdn.microsoft.com/en-us/library/dd135733.aspx 
    /// - Blob Service C# API - http://go.microsoft.com/fwlink/?LinkID=398944 
    /// - Delegating Access with Shared Access Signatures - http://azure.microsoft.com/en-us/documentation/articles/storage-dotnet-shared-access-signature-part-1/ 
    /// </summary> 
    public static class FileHandler
    {
        static string StorageConnectionString => ConfigurationManager.AppSettings["Storage:ConnectionString"];
        static string ContainerName => ConfigurationManager.AppSettings["Storage:ContainerName"];

        public static async Task<List<Uri>> GetFilesAsync()
        {
            var container = await GetContainerAsync();

            // To view the uploaded blob in a browser, you have two options. The first option is to use a Shared Access Signature (SAS) token to delegate  
            // access to the resource. See the documentation links at the top for more information on SAS. The second approach is to set permissions  
            // to allow public access to blobs in this container. Comment the line below to not use this approach and to use SAS. Then you can view the image  
            // using: https://[InsertYourStorageAccountNameHere].blob.core.windows.net/webappstoragedotnet-imagecontainer/FileName 

            // Gets all Block Blobs in the blobContainerName and passes them to the view
            List<Uri> allBlobs = new List<Uri>();
            foreach (BlobItem blob in container.GetBlobs())
            {
                if (blob.Properties.BlobType == BlobType.Block)
                    allBlobs.Add(container.GetBlobClient(blob.Name).Uri);
            }

            return allBlobs;
        }

        public static async Task<string> GetFileAsync(string filename)
        {
            var container = await GetContainerAsync();

            var blob = container.GetBlobClient(filename);

            return $"{blob?.Uri}";
        }

        public static async Task<BlobClient> UploadAsync(string filename, byte[] file)
        {
            var container = await GetContainerAsync();

            var blob = container.GetBlobClient(filename);
            var result = await blob.UploadAsync(new BinaryData(file), true);

            var version = !string.IsNullOrEmpty(result?.Value?.VersionId) ? result?.Value?.VersionId :
                    !string.IsNullOrEmpty(result?.Value?.LastModified.ToString()) ? result?.Value?.LastModified.ToString("yyyy-MM-dd-HH-mm") : "";


            if (string.IsNullOrEmpty(version))
            {
                throw new Exception("Ocurrió un error al guardar el archivo especificado!");
            }

            return blob;
        }

        private static async Task<BlobContainerClient> GetContainerAsync()
        {
            // Retrieve storage account information from connection string
            // How to create a storage connection string - http://msdn.microsoft.com/en-us/library/azure/ee758697.aspx

            var blobServiceClient = new BlobServiceClient(StorageConnectionString);
            var container = blobServiceClient.GetBlobContainerClient(ContainerName);
            await container.CreateIfNotExistsAsync(PublicAccessType.Blob);

            return container;
        }
    }



}


