using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Frends.GoogleCloudStorage.DeleteBucket.Definitions;

/// <summary>
/// Additional parameters.
/// </summary>
public class Options
{
    /// <summary>
    /// Whether to throw an error on failure.
    /// </summary>
    /// <example>true</example>
    [DefaultValue(true)]
    public bool ThrowErrorOnFailure { get; set; } = true;

    /// <summary>
    /// Determines whether the task should throw an exception if no bucket is found with the given name.
    /// </summary>
    /// <example>true</example>
    [DefaultValue(true)]
    public bool ThrowIfNoBucketFound { get; set; } = true;

    /// <summary>
    /// Overrides the error message on failure. If ThrowErrorOnFailure is true, the original exception is wrapped in a new exception with this message.
    /// </summary>
    /// <example>Bucket deletion failed: invalid credentials</example>
    [DisplayFormat(DataFormatString = "Text")]
    [DefaultValue("")]
    public string ErrorMessageOnFailure { get; set; } = string.Empty;
}
