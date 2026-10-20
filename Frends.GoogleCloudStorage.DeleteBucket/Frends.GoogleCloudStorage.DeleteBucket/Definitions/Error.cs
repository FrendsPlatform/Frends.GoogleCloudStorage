namespace Frends.GoogleCloudStorage.DeleteBucket.Definitions;

/// <summary>
/// Error details returned when the Task fails.
/// </summary>
public class Error
{
    /// <summary>
    /// Error message.
    /// </summary>
    /// <example>Bucket name is missing.</example>
    public string Message { get; set; }

    /// <summary>
    /// Exception associated with the failure.
    /// </summary>
    /// <example>System.ArgumentException: Bucket name is missing.</example>
    public object AdditionalInfo { get; set; }
}
