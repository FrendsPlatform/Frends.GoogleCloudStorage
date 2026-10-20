using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Frends.GoogleCloudStorage.DeleteObject.Attributes;
using Frends.GoogleCloudStorage.DeleteObject.Enums;

namespace Frends.GoogleCloudStorage.DeleteObject.Definitions;

/// <summary>
/// Input parameters.
/// </summary>
public class Input
{
    /// <summary>
    /// Id of the project.
    /// </summary>
    /// <example>your - project - id</example>
    [DefaultValue("your - project - id")]
    [DisplayFormat(DataFormatString = "Text")]
    [Required]
    public string ProjectId { get; set; }

    /// <summary>
    /// Name of the bucket.
    /// </summary>
    /// <example>your-unique-bucket-name</example>
    [DefaultValue("your-unique-bucket-name")]
    [DisplayFormat(DataFormatString = "Text")]
    [Required]
    public string BucketName { get; set; }

    /// <summary>
    /// File mask for the source files.
    /// </summary>
    /// <example>*.txt</example>
    [Required]
    public string Pattern { get; set; }

    /// <summary>
    /// Content to be uploaded to the Google Cloud Storage.
    /// </summary>
    /// <example>text/plain</example>
    public string ContentType { get; set; }

    /// <summary>
    /// Type of credentials input.
    /// </summary>
    /// <example>Json</example>
    [DefaultValue(CredentialsType.Json)]
    [EnumDataType(typeof(CredentialsType))]
    public CredentialsType CredentialsType { get; set; }

    /// <summary>
    /// Google Credentials in JSON format.
    /// </summary>
    /// <example>
    /// <code>
    /// {
    ///     "type": "service_account",
    ///     "project_id": "instant-test-stone-387712",
    ///     "private_key_id": "17d0g1b5a6e255c0ed67ef7213j8466c336edc55",
    ///     "private_key": "-----BEGIN PRIVATE KEY-----\jnejbUHBVudhcebuheuwhv....\n-----END PRIVATE KEY-----\n",
    ///     "client_email": "cloudstorage-testuser@instant-stone-387712.iam.gserviceaccount.com",
    ///     "client_id": "vjkornijvnerijvneri",
    ///     "auth_uri": "https://accounts.google.com/o/oauth2/auth",
    ///     "token_uri": "https://oauth2.googleapis.com/token",
    ///     "auth_provider_x509_cert_url": "https://www.googleapis.com/oauth2/v1/certs",
    ///     "client_x509_cert_url": "https://www.googleapis.com/robot/v1/metadata/x509/cloudstorage-testuser%40instant-stone-387712.iam.gserviceaccount.com",
    ///     "universe_domain": "googleapis.com"
    /// }
    /// </code>
    /// </example>
    [UIHint(nameof(CredentialsType), "", CredentialsType.Json)]
    [RequiredIf(nameof(CredentialsType), CredentialsType.Json)]
    [DisplayFormat(DataFormatString = "Json")]
    public string CredentialJson { get; set; }

    /// <summary>
    /// Full path to the json file which has credentials for Google.
    /// </summary>
    /// <example>C:\folder\google_credentials.json</example>
    [UIHint(nameof(CredentialsType), "", CredentialsType.FilePath)]
    [RequiredIf(nameof(CredentialsType), CredentialsType.FilePath)]
    [DisplayFormat(DataFormatString = "Text")]
    public string CredentialFilePath { get; set; }
}
