using Data = Google.Apis.Storage.v1.Data;

namespace Frends.GoogleCloudStorage.DeleteObject.Definitions;

/// <summary>
/// Information about a deleted object.
/// </summary>
public class DeletedObject
{
    internal DeletedObject(Data.Object obj)
    {
        Name = obj.Name;
        StorageClass = obj.StorageClass;
        Size = obj.Size;
        Bucket = obj.Bucket;
        TimeCreated = obj.TimeCreated;
    }

    /// <summary>
    /// Name of the deleted object.
    /// </summary>
    /// <example>file.txt</example>
    public string Name { get; private set; }

    /// <summary>
    /// Storage class for the deleted object.
    /// </summary>
    /// <example>STANDARD</example>
    public string StorageClass { get; private set; }

    /// <summary>
    /// DateTime of the object creation time.
    /// </summary>
    /// <example>2023-07-24T10:15:30Z</example>
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
}
