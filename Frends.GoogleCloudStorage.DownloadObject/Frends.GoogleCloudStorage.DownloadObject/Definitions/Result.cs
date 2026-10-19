namespace Frends.GoogleCloudStorage.DownloadObject.Definitions;

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
    /// Downloaded objects. Null when Success is false.
    /// </summary>
    /// <example>[ { Name = "file.txt", StorageClass = "STANDARD", TimeCreated = "2023-06-12T08:30:00Z", Bucket = "your-unique-bucket-123456789", Size = 1024 } ]</example>
    public List<DownloadedObject> Objects { get; private set; }

    /// <summary>
    /// Error details. Null when Success is true.
    /// </summary>
    /// <example>null</example>
    public Error Error { get; private set; }

    internal Result(bool success, List<DownloadedObject> objects = null, Error error = null)
    {
        Success = success;
        Objects = objects;
        Error = error;
    }
}
