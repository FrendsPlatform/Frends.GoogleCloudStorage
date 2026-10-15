using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using System.ComponentModel;
using System.Text.RegularExpressions;
using Frends.GoogleCloudStorage.DeleteObject.Definitions;
using Frends.GoogleCloudStorage.DeleteObject.Enums;
using Frends.GoogleCloudStorage.DeleteObject.Helpers;

namespace Frends.GoogleCloudStorage.DeleteObject;

/// <summary>
/// Main class for the Task.
/// </summary>
public static class GoogleCloudStorage
{
    /// <summary>
    /// Deletes file/s from Google Cloud Storage.
    /// [Documentation](https://tasks.frends.com/tasks/frends-tasks/Frends.GoogleCloudStorage.DeleteObject)
    /// </summary>
    /// <param name="input">Input parameters</param>
    /// <param name="options">Additional parameters</param>
    /// <param name="cancellationToken">CancellationToken is given by Frends</param>
    /// <returns>Object { bool Success, List&lt;DeletedObject&gt; Objects, Error Error { string Message, dynamic AdditionalInfo } }</returns>
    public static async Task<Result> DeleteObject(
        [PropertyTab] Input input,
        [PropertyTab] Options options,
        CancellationToken cancellationToken)
    {
        try
        {
            ValidationHandler.Run(input, options);

            var googleCredential = input.CredentialsType switch
            {
                CredentialsType.Json => GoogleCredential.FromJson(input.CredentialJson),
                CredentialsType.FilePath => GoogleCredential.FromFile(input.CredentialFilePath),
                _ => throw new ArgumentOutOfRangeException(nameof(input.CredentialsType), input.CredentialsType, "Unsupported credentials type."),
            };

            using var storage = await StorageClient.CreateAsync(googleCredential);

            var files = await FindMatchingFiles(storage, input.BucketName, input.Pattern, cancellationToken);
            var results = new List<DeletedObject>();
            foreach (var file in files)
                results.Add(await ExecuteDeleteSingleObjectAsync(storage, file, input.BucketName, cancellationToken));

            return new Result(true, results);
        }
        catch (Exception ex)
        {
            return ex.Handle(options);
        }
    }

    private static async Task<DeletedObject> ExecuteDeleteSingleObjectAsync(StorageClient storage, string file, string bucket,
        CancellationToken cancellationToken)
    {
        var deletedObject = await storage.GetObjectAsync(bucket, file, null, cancellationToken);
        await storage.DeleteObjectAsync(bucket, file, null, cancellationToken);
        return new DeletedObject(deletedObject);
    }

    private static async Task<List<string>> FindMatchingFiles(StorageClient client, string bucket, string pattern,
        CancellationToken cancellationToken)
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