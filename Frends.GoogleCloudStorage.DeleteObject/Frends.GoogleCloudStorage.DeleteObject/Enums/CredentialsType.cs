namespace Frends.GoogleCloudStorage.DeleteObject.Enums;

/// <summary>
/// Defines how Google Cloud credentials are provided.
/// </summary>
public enum CredentialsType
{
    /// <summary>
    /// Credentials are provided as a JSON string.
    /// </summary>
    Json,

    /// <summary>
    /// Credentials are read from a JSON file.
    /// </summary>
    FilePath,
}
