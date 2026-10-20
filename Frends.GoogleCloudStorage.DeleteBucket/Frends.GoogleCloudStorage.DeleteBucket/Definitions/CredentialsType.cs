namespace Frends.GoogleCloudStorage.DeleteBucket.Definitions;

/// <summary>
/// Defines how the Google credentials are provided.
/// </summary>
public enum CredentialsType
{
    /// <summary>
    /// Credentials are given as a JSON string.
    /// </summary>
    Json,

    /// <summary>
    /// Credentials are read from a JSON file.
    /// </summary>
    FilePath,
}
