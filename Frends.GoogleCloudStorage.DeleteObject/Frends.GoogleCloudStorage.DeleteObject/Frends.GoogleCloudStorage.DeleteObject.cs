using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using System.ComponentModel;
using System.Text.RegularExpressions;
using Frends.GoogleCloudStorage.DeleteObject.Definitions;

namespace Frends.GoogleCloudStorage.DeleteObject;

/// <summary>
/// Main class for the Task.
/// </summary>
public class GoogleCloudStorage
{
    /// <summary>
    /// Deletes file/s from Google Cloud Storage.
    /// [Documentation](https://tasks.frends.com/tasks/frends-tasks/Frends.GoogleCloudStorage.DeleteObject)
    /// </summary>
    /// <param name="input">Input parameters</param>
    /// <param name="cancellationToken">CancellationToken is given by Frends</param>
    /// <returns>List [ Result  object { string Name, string StorageClass, DateTime TimeCreated, string Bucket, ulong Size } ] </returns>
    public static async Task<List<Result>> DeleteObject([PropertyTab] Input input, CancellationToken cancellationToken)
    {
        GoogleCredential googleCredential = null;

        if (!string.IsNullOrEmpty(input.CredentialJson))
            googleCredential = GoogleCredential.FromJson(input.CredentialJson);
        else if (!string.IsNullOrEmpty(input.CredentialFilePath))
            googleCredential = GoogleCredential.FromFile(input.CredentialFilePath);
        else throw new ArgumentException("Credentials are missing.");

        if (string.IsNullOrEmpty(input.BucketName))
            throw new ArgumentException("Bucket name is missing.");

        using var storage = await StorageClient.CreateAsync(googleCredential);

        var foundBucketName = string.Empty;
        try
        {
            var bucket = await storage.GetBucketAsync(input.BucketName, null, cancellationToken);
            foundBucketName = bucket.Name;
        }
        catch (Exception)
        {
            if (string.IsNullOrEmpty(foundBucketName))
                throw new ArgumentException($"Bucket {input.BucketName} not found.");
            throw new Exception("Undefined error when accessing bucket.");
        }

        var files = await FindMatchingFiles(storage, foundBucketName, input.Pattern, cancellationToken);
        var results = new List<Result>();
        foreach (var file in files)
            results.Add(await ExecuteDeleteSingleObjectAsync(storage, file, foundBucketName, cancellationToken));

        return results;
    }

    private static async Task<Result> ExecuteDeleteSingleObjectAsync(StorageClient storage, string file, string bucket, CancellationToken cancellationToken)
    {
        var deletedObject = await storage.GetObjectAsync(bucket, file, null, cancellationToken);
        await storage.DeleteObjectAsync(bucket, file, null, cancellationToken);
        return new Result(deletedObject);
    }

    private static async Task<List<string>> FindMatchingFiles(StorageClient client, string bucket, string pattern, CancellationToken cancellationToken)
    {
        var files = new List<string>();
        var objects = client.ListObjectsAsync(bucket).WithCancellation(cancellationToken);
        await foreach (var file in objects)
        {
            if (FileMatchesMask(file.Name, pattern))
                files.Add(file.Name);
        }
        return files;
    }

    private static bool FileMatchesMask(string filename, string mask)
    {
        const string regexEscape = "<regex>";
        string pattern;

        //check is pure regex wished to be used for matching
        if (mask.StartsWith(regexEscape))
            //use substring instead of string.replace just in case some has regex like '<regex>//File<regex>' or something else like that
            pattern = mask.Substring(regexEscape.Length);
        else
        {
            pattern = mask.Replace(".", "\\.");
            pattern = pattern.Replace("*", ".*");
            pattern = pattern.Replace("?", ".+");
            pattern = string.Concat("^", pattern, "$");
        }

        return Regex.IsMatch(filename, pattern, RegexOptions.IgnoreCase);
    }
}

