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

    internal Result(bool success)
    {
        Success = success;
    }
}

