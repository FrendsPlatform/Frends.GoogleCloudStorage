namespace Frends.GoogleCloudStorage.DownloadObject.Enums;

/// <summary>
/// Types of Google Cloud credentials supported by the task.
/// </summary>
public enum CredentialType
{
    /// <summary>
    /// Credentials provided as a JSON string.
    /// </summary>
    Json,

    /// <summary>
    /// Credentials loaded from a JSON file.
    /// </summary>
    FilePath,
}
