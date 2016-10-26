/*
 *  Source: https://github.com/GoogleCloudPlatform/dotnet-docs-samples/blob/master/storage/api/StorageSample.cs
 * 
 */

using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Web;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Storage.v1;


namespace DailySports.BackOffice.Utilities
{
    class GoogleStorageService
    {
        private string bucketName = "cdn.dailyesports.tv";

        public void Delete(string fileName)
        {
            try
            {
                StorageService storage = CreateStorageClient();

                storage.Objects.Delete(bucketName, fileName).Execute();
            }  catch (Exception _)
            {
                //not found exception should not
            }
        }

        public void Upload(HttpPostedFileBase file, string fileName)
        {
            StorageService storage = CreateStorageClient();

            var body = new Google.Apis.Storage.v1.Data.Object();

            body.Name = fileName;

            var x = storage.Objects.Insert(
                bucket: bucketName,
                stream: file.InputStream,
                contentType: file.ContentType,
                body: body
            ).Upload();
        }

        private StorageService CreateStorageClient()
        {
           try {
                
                var credentials = Task.Run(() => GoogleCredential.GetApplicationDefaultAsync()).Result;

                if (credentials.IsCreateScopedRequired)
                {
                    credentials = credentials.CreateScoped(new[] { StorageService.Scope.DevstorageFullControl });
                }

                var serviceInitializer = new BaseClientService.Initializer()
                {
                    ApplicationName = "DailySportsMediaService",
                    HttpClientInitializer = credentials
                };

                return new StorageService(serviceInitializer);
            }
            catch (Exception _)
            {
                throw new InvalidOperationException("Error with Google API credentials. Check if GOOGLE_APPLICATION_CREDENTIALS environmental variable is set to file with credentials.");
            }
        }
    }
}
