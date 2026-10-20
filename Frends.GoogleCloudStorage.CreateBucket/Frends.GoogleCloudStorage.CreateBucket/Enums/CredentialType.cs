namespace Frends.GoogleCloudStorage.CreateBucket.Enums;

/// <summary>
/// Supported credential input types.
/// </summary>
public enum CredentialType
{
    /// <summary>
    /// Credentials provided as JSON content.
    /// </summary>
    Json,

    /// <summary>
    /// Credentials provided as a path to a JSON file.
    /// </summary>
    FilePath,
}
