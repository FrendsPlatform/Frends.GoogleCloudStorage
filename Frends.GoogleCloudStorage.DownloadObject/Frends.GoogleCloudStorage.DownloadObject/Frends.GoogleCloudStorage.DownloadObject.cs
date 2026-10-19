using System.ComponentModel;
using System.Text.RegularExpressions;
using Frends.GoogleCloudStorage.DownloadObject.Definitions;
using Frends.GoogleCloudStorage.DownloadObject.Enums;
using Frends.GoogleCloudStorage.DownloadObject.Helpers;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;

namespace Frends.GoogleCloudStorage.DownloadObject;

/// <summary>
/// Main class for the Task.
/// </summary>
public static class GoogleCloudStorage
{
    /// <summary>
    /// Downloads files from Google Cloud Storage.
    /// [Documentation](https://tasks.frends.com/tasks/frends-tasks/Frends.GoogleCloudStorage.DownloadObject)
    /// </summary>
    /// <param name="input">Input parameters</param>
    /// <param name="options">Additional parameters</param>
    /// <param name="cancellationToken">CancellationToken is given by Frends</param>
    /// <returns>Object { bool Success, List&lt;DownloadedObject&gt; Objects, Object Error { string Message, Exception AdditionalInfo } }</returns>
    public static async Task<Result> DownloadObject([PropertyTab] Input input, [PropertyTab] Options options, CancellationToken cancellationToken)
    {
        try
        {
            ValidationHandler.Run(input);

            var googleCredential = input.CredentialsType switch
            {
                CredentialType.Json => GoogleCredential.FromJson(input.CredentialJson),
                CredentialType.FilePath => GoogleCredential.FromFile(input.CredentialFilePath),
                _ => throw new ArgumentOutOfRangeException(nameof(input.CredentialsType), input.CredentialsType, "Unsupported credentials type."),
            };

            using var storage = await StorageClient.CreateAsync(googleCredential);

            var files = await FindMatchingFiles(storage, input.BucketName, input.Pattern, cancellationToken);
            var results = new List<DownloadedObject>();
            foreach (var file in files)
                results.Add(await ExecuteDownloadSingleObjectAsync(storage, file, input.BucketName, input, cancellationToken));

            return new Result(true, results);
        }
        catch (Exception ex)
        {
            return ex.Handle(options);
        }
    }

    private static async Task<DownloadedObject> ExecuteDownloadSingleObjectAsync(StorageClient storage, string file, string bucket, Input input, CancellationToken cancellationToken)
    {
        using var fs = File.Open(Path.Combine(input.Directory, Path.GetFileName(file)), FileMode.OpenOrCreate);
        var result = await storage.DownloadObjectAsync(bucket, file, fs, null, cancellationToken);
        return new DownloadedObject(result);
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

        // Check if a pure regex is wished to be used for matching.
        if (mask.StartsWith(regexEscape))
        {
            // Use Substring to avoid removing a regexEscape token embedded in the regex.
            pattern = mask.Substring(regexEscape.Length);
        }
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
