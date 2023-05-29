using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Frends.GoogleCloudStorage.CreateBucket.Enums;

namespace Frends.GoogleCloudStorage.CreateBucket.Definitions;

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
    public string ProjectId { get; set; }

    /// <summary>
    /// Name for the bucket. If left empty NewGuid() method is used to create the bucket name.
    /// </summary>
    /// <example>your-unique-bucket-name</example>
    [DefaultValue("your-unique-bucket-name")]
    [DisplayFormat(DataFormatString = "Text")]
    public string BucketName { get; set; }

    /// <summary>
    /// When enabled the Task will add guid to the end of the bucket name with NewGuid() method. This will make sure that the bucket name is unique.
    /// </summary>
    [DefaultValue(false)]
    public bool AddGuidToBucketName { get; set; }

    /// <summary>
    /// Location
    /// </summary>
    /// <example>us-west1</example>
    [DefaultValue("us-west1")]
    [DisplayFormat(DataFormatString = "Text")]
    public string Location { get; set; }

    /// <summary>
    /// Storage class for the bucket.
    /// </summary>
    /// <example>StorageClassType.STANDARD</example>
    [DefaultValue(StorageClassType.STANDARD)]
    public StorageClassType StorageClass { get; set; }

    /// <summary>
    /// Google Credentials in JSON format.
    /// </summary>
    /// <example>
    /// <code>
    /// {
    ///     "type": "service_account",
    ///	    "project_id": "instant-test-stone-387712",
    ///	    "private_key_id": "17d0g1b5a6e255c0ed67ef7213j8466c336edc55",
    ///	    "private_key": "-----BEGIN PRIVATE KEY-----\jnejbUHBVudhcebuheuwhv....\n-----END PRIVATE KEY-----\n",
    ///	    "client_email": "cloudstorage-testuser@instant-stone-387712.iam.gserviceaccount.com",
    ///	    "client_id": "vjkornijvnerijvneri",
    ///	    "auth_uri": "https://accounts.google.com/o/oauth2/auth",
    ///	    "token_uri": "https://oauth2.googleapis.com/token",
    ///	    "auth_provider_x509_cert_url": "https://www.googleapis.com/oauth2/v1/certs",
    ///	    "client_x509_cert_url": "https://www.googleapis.com/robot/v1/metadata/x509/cloudstorage-testuser%40instant-stone-387712.iam.gserviceaccount.com",
    ///	    "universe_domain": "googleapis.com"
    /// }
    /// </code>
    /// </example>
    [DisplayFormat(DataFormatString = "Json")]
    public string CredentialJson { get; set; }

    /// <summary>
    /// Full path to the json file which has credentials for Google.
    /// </summary>
    /// <example>C:\folder\google_credentials.json</example>
    [DisplayFormat(DataFormatString = "Text")]
    public string CredentialFilePath { get; set; }
}

