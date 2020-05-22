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
        private static IAmazonS3 s3Client;

        public S3Client(string accessKey, string secretKey, RegionEndpoint regionEndpoint)
        {
            var awsCredentials = new Amazon.Runtime.BasicAWSCredentials(accessKey, secretKey);

            s3Client = new AmazonS3Client(awsCredentials, regionEndpoint);
        }

        public static async Task UploadFileAsync(string filePath, string bucketName, string keyName)
        {
            try
            {
                var fileTransferUtility = new TransferUtility(s3Client);

                await fileTransferUtility.UploadAsync(filePath, bucketName, keyName);
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine("Error encountered on server. Message:'{0}' when writing an object", e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unknown encountered on server. Message:'{0}' when writing an object", e.Message);
            }

        }
    }
}
