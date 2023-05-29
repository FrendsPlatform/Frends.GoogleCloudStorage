using Google.Apis.Storage.v1.Data;
namespace Frends.GoogleCloudStorage.CreateBucket.Definitions;

/// <summary>
/// Result class.
/// </summary>
public class Result
{
    /// <summary>
    /// Bucket name of the newly created bucket.
    /// </summary>
    /// <exmaple>test-bucket123456789</exmaple>
    public string BucketName { get; private set; }

    /// <summary>
    /// Location of the bucket.
    /// </summary>
    /// <example>US-CENTRAL1</example>
    public string Location { get; private set; }

    /// <summary>
    /// Storage class of the bucket.
    /// </summary>
    /// <example>STARNDARD</example>
    public string StorageClass { get; private set; }

    /// <summary>
    /// DateTime of the created bucket.
    /// </summary>
    /// <example>26/05/2023 15.47.51</example>
    public DateTime? TimeCreated { get; private set; }

    internal Result(Bucket bucket)
    {
        BucketName = bucket.Name;
        Location = bucket.Location;
        StorageClass = bucket.StorageClass;
        TimeCreated = bucket.TimeCreated;
    }
}

