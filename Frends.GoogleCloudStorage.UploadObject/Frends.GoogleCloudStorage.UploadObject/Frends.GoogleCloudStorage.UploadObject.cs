using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.Extensions.FileSystemGlobbing;
using Microsoft.Extensions.FileSystemGlobbing.Abstractions;
using System.ComponentModel;
using Frends.GoogleCloudStorage.UploadObject.Definitions;

namespace Frends.GoogleCloudStorage.UploadObject;

/// <summary>
/// Main class for the Task.
/// </summary>
public class GoogleCloudStorage
{


    /// <summary>
    /// Uploads files to Google Cloud Storage.
    /// [Documentation](https://tasks.frends.com/tasks/frends-tasks/Frends.GoogleCloudStorage.UploadObject)
    /// </summary>
    /// <frendsdocs>
    /// UploadObject uses pattern matching for finding files to be uploaded to GoogleCloudStorage.
    ///
    ///    The search starts from the root directory defined in the input parameters.
    ///
    ///
    ///    * to match one or more characters in a path segment
    ///
    ///   ** to match any number of path segments, including none
    ///
    ///    Examples:
    ///
    /// **\output\*\temp\*.txt matches:
    ///
    ///    test\subfolder\output\2015\temp\file.txt
    ///    production\output\2016\temp\example.txt
    ///
    /// **\temp* matches
    ///
    ///    prod\test\temp123.xml
    ///    test\temp234.xml
    ///
    /// subfolder\**\temp\*.xml matches
    ///
    ///    subfolder\temp\test.xml
    ///    subfolder\foo\bar\is\here\temp\test.xml
    /// </frendsdocs>
    /// <param name="input">Input parameters</param>
    /// <param name="cancellationToken">CancellationToken is given by Frends</param>
    /// <returns>List [ Result object { string Name, string StorageClass, DateTime TimeCreated, string Bucket, ulong Size } ] </returns>
    public static async Task<List<Result>> UploadObject([PropertyTab] Input input, CancellationToken cancellationToken)
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

        var bucket = storage.ListBuckets(input.ProjectId, null).FirstOrDefault(n => n.Name.Equals(input.BucketName)).Name;
        if (string.IsNullOrEmpty(bucket))
            throw new ArgumentException($"Bucket {input.BucketName} not found.");

        var matchResults = FindMatchingFiles(input.Directory, input.Pattern);
        var files = matchResults.Files.Select(match => Path.Combine(input.Directory, match.Path)).ToArray();
        var results = new List<Result>();
        foreach (var file in files)
            results.Add(await ExecuteUploadSingleObjectAsync(storage, file, bucket, input, cancellationToken));

        return results;
    }

    private static async Task<Result> ExecuteUploadSingleObjectAsync(StorageClient storage, string file, string bucket, Input input, CancellationToken cancellationToken)
    {
        var objectName = Path.GetFileName(file);
        var result = await storage.UploadObjectAsync(bucket, objectName, input.ContentType, new MemoryStream(File.ReadAllBytes(file)), null, cancellationToken);
        return new Result(result);
    }

    private static PatternMatchingResult FindMatchingFiles(string directoryPath, string pattern)
    {
        // Check the user can access the folder
        // This will return false if the path does not exist or you do not have read permissions.
        if (!Directory.Exists(directoryPath))
        {
            throw new Exception($"Directory does not exist or you do not have read access. Tried to access directory '{directoryPath}'");
        }

        var matcher = new Matcher();
        matcher.AddInclude(pattern);
        var results = matcher.Execute(new DirectoryInfoWrapper(new DirectoryInfo(directoryPath)));
        return results;
    }
}

