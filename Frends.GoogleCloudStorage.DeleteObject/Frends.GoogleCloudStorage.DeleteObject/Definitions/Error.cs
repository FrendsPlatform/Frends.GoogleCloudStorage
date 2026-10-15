namespace Frends.GoogleCloudStorage.DeleteObject.Definitions;

/// <summary>
/// Error details.
/// </summary>
public class Error
{
    /// <summary>
    /// Summary of the error.
    /// </summary>
    /// <example>Bucket name is missing.</example>
    public string Message { get; set; }

    /// <summary>
    /// The exception that caused the error.
    /// </summary>
    /// <example>System.ArgumentException: Bucket name is missing.</example>
    public dynamic AdditionalInfo { get; set; }
}
