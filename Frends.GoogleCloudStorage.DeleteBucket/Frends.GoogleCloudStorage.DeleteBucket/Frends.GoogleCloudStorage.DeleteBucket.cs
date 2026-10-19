using System.ComponentModel;
using Frends.GoogleCloudStorage.DeleteBucket.Definitions;
using Frends.GoogleCloudStorage.DeleteBucket.Helpers;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;

namespace Frends.GoogleCloudStorage.DeleteBucket;

/// <summary>
/// Main class for the Task.
/// </summary>
public static class GoogleCloudStorage
{
    /// <summary>
    /// Deletes a bucket from Google Cloud Storage.
    /// [Documentation](https://tasks.frends.com/tasks/frends-tasks/Frends.GoogleCloudStorage.DeleteBucket)
    /// </summary>
    /// <param name="input">Bucket input parameters</param>
    /// <param name="options">Additional parameters.</param>
    /// <param name="cancellationToken">CancellationToken is given by Frends</param>
    /// <returns>object { bool Success, object { string Message, Exception AdditionalInfo } Error }</returns>
    public static async Task<Result> DeleteBucket([PropertyTab] Input input, [PropertyTab] Options options, CancellationToken cancellationToken)
    {
        try
        {
            ValidationHandler.Run(input, options);

            var googleCredential = input.CredentialsType == CredentialsType.Json
                ? GoogleCredential.FromJson(input.CredentialJson)
                : GoogleCredential.FromFile(input.CredentialFilePath);

            using var storage = await StorageClient.CreateAsync(googleCredential);

            var bucket = storage.ListBuckets(input.ProjectId, null).FirstOrDefault(n => n.Name.Equals(input.BucketName));
            if (bucket != null)
            {
                var deleteBucketOptions = new DeleteBucketOptions
                {
                    DeleteObjects = input.DeleteObjects,
                    RetryOptions = RetryOptions.IdempotentRetryOptions,
                };

                await storage.DeleteBucketAsync(bucket, deleteBucketOptions, cancellationToken);
                return new Result(true);
            }

            if (options.ThrowIfNoBucketFound)
                throw new ArgumentException("No bucket found.");
            return new Result(false);
        }
        catch (Exception ex)
        {
            return ex.Handle(options);
        }
    }
}
