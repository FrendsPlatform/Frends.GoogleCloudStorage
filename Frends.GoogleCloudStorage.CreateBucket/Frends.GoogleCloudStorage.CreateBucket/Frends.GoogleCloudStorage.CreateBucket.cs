using Google.Apis.Auth.OAuth2;
using Google.Apis.Storage.v1.Data;
using Google.Cloud.Storage.V1;
using System.ComponentModel;
using Frends.GoogleCloudStorage.CreateBucket.Definitions;

namespace Frends.GoogleCloudStorage.CreateBucket;

/// <summary>
/// Main class for the Task.
/// </summary>
public class GoogleCloudStorage
{


    /// <summary>
    /// Creates a bucket to Google Cloud Storage.
    /// [Documentation](https://tasks.frends.com/tasks/frends-tasks/Frends.GoogleCloudStorage.CreateBucket)
    /// </summary>
    /// <param name="input">Bucket input parameters</param>
    /// <param name="cancellationToken">CancellationToken is given by Frends</param>
    /// <returns>Result object { string BucketName, string Location, string StorageClass, DateTime TimeCreated } </returns>
    public static async Task<Result> CreateBucket([PropertyTab] Input input, 
        CancellationToken cancellationToken)
    {
        GoogleCredential googleCredential = null; 
        
        if (!string.IsNullOrEmpty(input.CredentialJson))
            googleCredential = GoogleCredential.FromJson(input.CredentialJson);
        else if (!string.IsNullOrEmpty(input.CredentialFilePath))
            googleCredential = GoogleCredential.FromFile(input.CredentialFilePath);

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
            StorageClass = input.StorageClass.ToString()
        };

        var newlyCreatedBucket = await storage.CreateBucketAsync(input.ProjectId, bucket, null, cancellationToken);
        return new Result(newlyCreatedBucket);
    }
}

