using Data = Google.Apis.Storage.v1.Data;

namespace Frends.GoogleCloudStorage.DownloadObject.Definitions;

/// <summary>
/// Result class.
/// </summary>
public class Result
{
    /// <summary>
    /// Name of the uploaded object.
    /// </summary>
    /// <example>file.txt</example>
    public string Name { get; private set; }

    /// <summary>
    /// Storage class for the uploaded object.
    /// </summary>
    /// <example>STANDARD</example>
    public string StorageClass { get; private set; }

    /// <summary>
    /// DateTime of the object creation time.
    /// </summary>
    /// <example></example>
    public DateTime? TimeCreated { get; private set; }

    /// <summary>
    /// Name of the bucket where the object was uploaded.
    /// </summary>
    /// <example>your-unique-bucket-123456789</example>
    public string Bucket { get; private set; }

    /// <summary>
    /// Size of the content inside the uploaded object.
    /// </summary>
    /// <example></example>
    public ulong? Size { get; private set; }

    internal Result(Data.Object obj)
    {
        Name = obj.Name;
        StorageClass = obj.StorageClass;
        Size = obj.Size;
        Bucket = obj.Bucket;
        TimeCreated = obj.TimeCreated;
    }
}

