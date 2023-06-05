namespace Frends.GoogleCloudStorage.CreateBucket.Enums;
/// <summary>
/// Types of storage class used in Google Cloud Storage.
/// </summary>
public enum StorageClassType
{
    /// <summary>
    /// Standard storage is best for data that is frequently accessed ("hot" data) and/or stored for only brief periods of time.
    /// </summary>
    STANDARD,
    /// <summary>
    /// Nearline storage is a low-cost, highly durable storage service for storing infrequently accessed data.
    /// </summary>
    NEARLINE,
    /// <summary>
    /// Coldline storage is a very-low-cost, highly durable storage service for storing infrequently accessed data.
    /// </summary>
    COLDLINE,
    /// <summary>
    /// Archive storage is the lowest-cost, highly durable storage service for data archiving, online backup, and disaster recovery.
    /// </summary>
    ARCHIVE
}

