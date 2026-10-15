namespace Frends.GoogleCloudStorage.DeleteBucket.Definitions;

/// <summary>
/// Result class.
/// </summary>
public class Result
{
    /// <summary>
    /// Result of the operation.
    /// </summary>
    /// <example>true</example>
    public bool Success { get; private set; }

    /// <summary>
    /// Error details. Null when the operation succeeds.
    /// </summary>
    /// <example>Bucket name is missing.</example>
    public Error Error { get; private set; }

    /// <summary>
    /// Initializes a result with its success state and optional error details.
    /// </summary>
    /// <param name="success">Whether the operation completed successfully.</param>
    /// <param name="error">Error details when the operation failed.</param>
    public Result(bool success, Error error = null)
    {
        Success = success;
        Error = error;
    }
}
