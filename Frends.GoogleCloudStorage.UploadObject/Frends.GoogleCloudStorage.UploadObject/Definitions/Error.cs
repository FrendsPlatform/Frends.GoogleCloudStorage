namespace Frends.GoogleCloudStorage.UploadObject.Definitions;

/// <summary>
/// Error details.
/// </summary>
public class Error
{
    /// <summary>
    /// Error message.
    /// </summary>
    /// <example>Bucket name is missing.</example>
    public string Message { get; internal set; }

    /// <summary>
    /// Additional exception details.
    /// </summary>
    /// <example>ArgumentException</example>
    public Exception AdditionalInfo { get; internal set; }
}
