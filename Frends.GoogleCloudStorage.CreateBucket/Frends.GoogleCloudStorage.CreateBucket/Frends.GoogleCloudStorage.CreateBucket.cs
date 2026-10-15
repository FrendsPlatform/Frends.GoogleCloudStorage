using System.ComponentModel;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Storage.v1.Data;
using Google.Cloud.Storage.V1;
using Frends.GoogleCloudStorage.CreateBucket.Definitions;
using Frends.GoogleCloudStorage.CreateBucket.Enums;
using Frends.GoogleCloudStorage.CreateBucket.Helpers;

namespace Frends.GoogleCloudStorage.CreateBucket;

/// <summary>
/// Main class for the Task.
/// </summary>
public static class GoogleCloudStorage
{
    /// <summary>
    /// Creates a bucket to Google Cloud Storage.
    /// [Documentation](https://tasks.frends.com/tasks/frends-tasks/Frends.GoogleCloudStorage.CreateBucket)
    /// </summary>
    /// <param name="input">Bucket input parameters</param>
    /// <param name="options">Additional parameters</param>
    /// <param name="cancellationToken">CancellationToken is given by Frends</param>
    /// <returns>Result object { bool Success, string BucketName, string Location, string StorageClass, DateTime TimeCreated, object Error { string Message, Exception AdditionalInfo } } </returns>
    public static async Task<Result> CreateBucket([PropertyTab] Input input, [PropertyTab] Options options, CancellationToken cancellationToken)
    {
        try
        {
            ValidationHandler.Run(input, options);

            var googleCredential = input.CredentialsType switch
            {
                CredentialType.Json => GoogleCredential.FromJson(input.CredentialJson),
                CredentialType.FilePath => GoogleCredential.FromFile(input.CredentialFilePath),
                _ => throw new ArgumentOutOfRangeException(nameof(input.CredentialsType), input.CredentialsType, "Unsupported credential type."),
            };

            using var storage = await StorageClient.CreateAsync(googleCredential);

            var bucketName = input.BucketName;
            if (string.IsNullOrEmpty(bucketName))
                bucketName = Guid.NewGuid().ToString();
            else if (input.AddGuidToBucketName)
                bucketName = $"{bucketName}-{Guid.NewGuid()}";

            var bucket = new Bucket
            {
                Location = input.Location,
                Name = bucketName,
                StorageClass = input.StorageClass.ToString(),
            };

            var newlyCreatedBucket = await storage.CreateBucketAsync(input.ProjectId, bucket, null, cancellationToken);
            return new Result(true, newlyCreatedBucket);
        }
        catch (Exception ex)
        {
            return ex.Handle(options);
        }
    }
}
