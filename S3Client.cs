using Amazon;
using Amazon.Runtime.CredentialManagement;
using Amazon.S3;
using Amazon.S3.Transfer;
using Amazon.Util;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AwsUtils
{
    public class S3Client
    {
        private readonly IAmazonS3 s3Client;

        public S3Client(string accessKey, string secretKey, RegionEndpoint regionEndpoint)
        {
            var awsCredentials = new Amazon.Runtime.BasicAWSCredentials(accessKey, secretKey);

            s3Client = new AmazonS3Client(awsCredentials, regionEndpoint);
        }

        public async Task UploadFileAsync(string filePath, string bucketName, string keyName, string contentType = null)
        {
            try
            {
                var fileTransferUtility = new TransferUtility(s3Client);

                var req = new TransferUtilityUploadRequest()
                {
                    FilePath = filePath,
                    BucketName = bucketName,
                    Key = keyName,
                    
                };
    
                if(contentType != null)
                {
                    req.ContentType = contentType;
                }

                await fileTransferUtility.UploadAsync(filePath, bucketName, keyName);
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine($"Error encountered on server. Message:'{e.Message}' when writing an object");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unknown encountered on server. Message:'{e.Message}' when writing an object");
            }

        }

        public async Task DownloadFileAsync(string filePath, string bucketName, string keyName)
        {
            try
            {
                var fileTransferUtility = new TransferUtility(s3Client);

                await fileTransferUtility.DownloadAsync(filePath, bucketName, keyName);
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine($"Error encountered on server. Message:'{e.Message}' when reading an object");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unknown encountered on server. Message:'{e.Message}' when reading an object");
            }

        }
    }
}
