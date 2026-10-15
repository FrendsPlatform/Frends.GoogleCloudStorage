namespace Frends.GoogleCloudStorage.DeleteObject.Definitions;

/// <summary>
/// Result class.
/// </summary>
public class Result
{
    /// <summary>
    /// Indicates whether the operation completed successfully.
    /// </summary>
    /// <example>true</example>
    public bool Success { get; private set; }

    /// <summary>
    /// List of the deleted objects. Empty when Success is false.
    /// </summary>
    /// <example>[ { "Name": "file.txt", "StorageClass": "STANDARD", "Bucket": "your-unique-bucket-name", "Size": 1024 } ]</example>
    public List<DeletedObject> Objects { get; private set; }

    /// <summary>
    /// Error details. Null when Success is true.
    /// </summary>
    /// <example>null</example>
    public Error Error { get; private set; }

    internal Result(bool success, List<DeletedObject> objects = null, Error error = null)
    {
        Success = success;
        Objects = objects ?? new List<DeletedObject>();
        Error = error;
    }
}
