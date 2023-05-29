namespace Frends.GoogleCloudStorage.CreateBucket.Definitions;

/// <summary>
/// Supported credential types for URL signing
/// </summary>
public enum CredentialType
{
    /// <summary>
    /// Agent's personal credentials are used. 
    /// </summary>
    None,
    /// <summary>
    /// Google OAuth 2.0 credential for accessing protected resources using an access token.
    /// </summary>
    ServiceAccountCredentials,
    /// <summary>
    /// Allows a service account or user credential to impersonate a service account.
    /// </summary>
    ImpersonatedCredentials,
    /// <summary>
    /// Google OAuth 2.0 credential for accessing protected resources using an access token.
    /// </summary>
    ComputeCredentials,
    /// <summary>
    /// Credential for authorizing calls using OAuth 2.0. It is a convenience wrapper
    /// that allows handling of different types of credentials (like Google.Apis.Auth.OAuth2.ServiceAccountCredential,
    /// Google.Apis.Auth.OAuth2.ComputeCredential or Google.Apis.Auth.OAuth2.UserCredential)
    /// in a unified way.
    /// </summary>
    GoogleCredentials
}

