using Data = Google.Apis.Storage.v1.Data;

namespace Frends.GoogleCloudStorage.DownloadObject.Definitions;

/// <summary>
/// Information about a downloaded object.
/// </summary>
public class DownloadedObject
{
    /// <summary>
    /// Name of the downloaded object.
    /// </summary>
    /// <example>file.txt</example>
    public string Name { get; private set; }

    /// <summary>
    /// Storage class for the downloaded object.
    /// </summary>
    /// <example>STANDARD</example>
    public string StorageClass { get; private set; }

    /// <summary>
    /// DateTime of the object creation time.
    /// </summary>
    /// <example>2023-06-12T08:30:00Z</example>
    public DateTime? TimeCreated { get; private set; }

    /// <summary>
    /// Name of the bucket where the object was downloaded from.
    /// </summary>
    /// <example>your-unique-bucket-123456789</example>
    public string Bucket { get; private set; }

    /// <summary>
    /// Size of the content inside the downloaded object in bytes.
    /// </summary>
    /// <example>1024</example>
    public ulong? Size { get; private set; }

    internal DownloadedObject(Data.Object obj)
    {
        Name = obj.Name;
        StorageClass = obj.StorageClass;
        Size = obj.Size;
        Bucket = obj.Bucket;
        TimeCreated = obj.TimeCreated;
    }
}
