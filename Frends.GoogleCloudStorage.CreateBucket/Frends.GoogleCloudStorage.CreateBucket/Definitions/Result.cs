using Google.Apis.Storage.v1.Data;

namespace Frends.GoogleCloudStorage.CreateBucket.Definitions;

/// <summary>
/// Result class.
/// </summary>
public class Result
{
    internal Result(bool success, Bucket bucket = null, Error error = null)
    {
        Success = success;
        BucketName = bucket?.Name;
        Location = bucket?.Location;
        StorageClass = bucket?.StorageClass;
        TimeCreated = bucket?.TimeCreated;
        Error = error;
    }

    /// <summary>
    /// Result of the operation.
    /// </summary>
    /// <example>true</example>
    public bool Success { get; private set; }

    /// <summary>
    /// Bucket name of the newly created bucket.
    /// </summary>
    /// <example>test-bucket123456789</example>
    public string BucketName { get; private set; }

    /// <summary>
    /// Location of the bucket.
    /// </summary>
    /// <example>US-CENTRAL1</example>
    public string Location { get; private set; }

    /// <summary>
    /// Storage class of the bucket.
    /// </summary>
    /// <example>STANDARD</example>
    public string StorageClass { get; private set; }

    /// <summary>
    /// DateTime of the created bucket.
    /// </summary>
    /// <example>26/05/2023 15.47.51</example>
    public DateTime? TimeCreated { get; private set; }

    /// <summary>
    /// Error details. Null when Success is true.
    /// </summary>
    /// <example>null</example>
    public Error Error { get; private set; }
}
