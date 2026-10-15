using Data = Google.Apis.Storage.v1.Data;

namespace Frends.GoogleCloudStorage.UploadObject.Definitions;

/// <summary>
/// Metadata for an uploaded object.
/// </summary>
public class UploadedObject
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
    /// <example>2025-10-17T11:51:29Z</example>
    public DateTime? TimeCreated { get; private set; }

    /// <summary>
    /// Name of the bucket where the object was uploaded.
    /// </summary>
    /// <example>your-unique-bucket-123456789</example>
    public string Bucket { get; private set; }

    /// <summary>
    /// Size of the content inside the uploaded object.
    /// </summary>
    /// <example>1024</example>
    public ulong? Size { get; private set; }

    internal UploadedObject(Data.Object obj)
    {
        Name = obj.Name;
        StorageClass = obj.StorageClass;
        Size = obj.Size;
        Bucket = obj.Bucket;
        TimeCreated = obj.TimeCreated;
    }
}
