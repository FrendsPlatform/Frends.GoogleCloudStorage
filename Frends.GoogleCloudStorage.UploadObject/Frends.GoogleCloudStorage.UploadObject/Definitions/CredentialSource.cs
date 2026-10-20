namespace Frends.GoogleCloudStorage.UploadObject.Definitions;

/// <summary>
/// The source of Google Cloud credentials.
/// </summary>
public enum CredentialSource
{
    /// <summary>
    /// Credentials are provided as a JSON string.
    /// </summary>
    Json,

    /// <summary>
    /// Credentials are provided as a file path.
    /// </summary>
    FilePath,
}
