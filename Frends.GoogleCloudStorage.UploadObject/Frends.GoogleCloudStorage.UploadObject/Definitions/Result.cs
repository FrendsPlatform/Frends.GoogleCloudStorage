namespace Frends.GoogleCloudStorage.UploadObject.Definitions;

/// <summary>
/// Result class.
/// </summary>
public class Result
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Result"/> class.
    /// </summary>
    /// <param name="success">Whether the upload operation completed successfully.</param>
    /// <param name="error">Error details when the operation failed.</param>
    public Result(bool success, Error error = null)
    {
        Success = success;
        Error = error;
    }

    /// <summary>
    /// Indicates whether the operation completed successfully.
    /// </summary>
    /// <example>true</example>
    public bool Success { get; private set; }

    /// <summary>
    /// Error details. Null when the operation succeeds.
    /// </summary>
    /// <example>null</example>
    public Error Error { get; private set; }

    /// <summary>
    /// Metadata for the uploaded objects.
    /// </summary>
    /// <example>List&lt;UploadedObject&gt; { object { string Name, string StorageClass, DateTime? TimeCreated, string Bucket, ulong? Size } }</example>
    public List<UploadedObject> UploadedObjects { get; internal set; } = [];
}
