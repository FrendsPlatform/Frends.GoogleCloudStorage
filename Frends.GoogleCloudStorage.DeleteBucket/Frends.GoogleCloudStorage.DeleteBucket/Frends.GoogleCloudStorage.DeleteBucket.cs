using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using System.ComponentModel;
using Frends.GoogleCloudStorage.DeleteBucket.Definitions;

namespace Frends.GoogleCloudStorage.DeleteBucket;

/// <summary>
/// Main class for the Task.
/// </summary>
public class GoogleCloudStorage
{


    /// <summary>
    /// Deletes a bucket from Google Cloud Storage.
    /// [Documentation](https://tasks.frends.com/tasks/frends-tasks/Frends.GoogleCloudStorage.DeleteBucket)
    /// </summary>
    /// <param name="input">Bucket input parameters</param>
    /// <param name="cancellationToken">CancellationToken is given by Frends</param>
    /// <returns>Result object { string BucketName, string Location, string StorageClass, DateTime TimeCreated } </returns>
    public static async Task<Result> DeleteBucket([PropertyTab] Input input, CancellationToken cancellationToken)
    {
        GoogleCredential googleCredential = null;

        if (!string.IsNullOrEmpty(input.CredentialJson))
            googleCredential = GoogleCredential.FromJson(input.CredentialJson);
        else if (!string.IsNullOrEmpty(input.CredentialFilePath))
            googleCredential = GoogleCredential.FromFile(input.CredentialFilePath);
        else throw new ArgumentException("Credentials are missing.");

        if (string.IsNullOrEmpty(input.BucketName))
            throw new ArgumentException("Bucket name is missing.");

        using var storage = await StorageClient.CreateAsync(googleCredential);

        var bucket = storage.ListBuckets(input.ProjectId, null).FirstOrDefault(n => n.Name.Equals(input.BucketName));
        if (bucket != null)
        {
            var options = new DeleteBucketOptions
            {
                DeleteObjects = input.DeleteObjects,
                RetryOptions = RetryOptions.IdempotentRetryOptions
            };

            await storage.DeleteBucketAsync(bucket, options, cancellationToken);
            return new Result(true);
        }

        if (input.ThrowIfNoBucketFound)
            throw new ArgumentException("No bucket found.");
        return new Result(false);
    }
}

