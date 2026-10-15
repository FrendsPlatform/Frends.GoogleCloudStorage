namespace Frends.GoogleCloudStorage.UploadObject.Definitions;

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
    /// Error details. Null when the operation succeeds.
    /// </summary>
    /// <example>null</example>
    public Error Error { get; private set; }

    /// <summary>
    /// Metadata for the uploaded objects.
    /// </summary>
    /// <example>One uploaded object</example>
    public List<UploadedObject> UploadedObjects { get; internal set; } = [];

    public Result(bool success, Error error = null)
    {
        Success = success;
        Error = error;
    }
}
