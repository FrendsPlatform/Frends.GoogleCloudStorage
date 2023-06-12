using NUnit.Framework;
using System;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Google.Apis.Storage.v1.Data;
using Frends.GoogleCloudStorage.DownloadObject.Definitions;

namespace Frends.GoogleCloudStorage.DownloadObject.Tests;

[TestFixture]
class UnitTests
{
    /// <summary>
    /// Needs credentials set in environment variables.
    /// </summary>
    private readonly string _credentialsBase64 = Environment.GetEnvironmentVariable("Frends_GoogleCloudStorage_CredJson");
    private readonly dynamic _details = new
    {
        ProjectId = "instant-stone-387712",
        BucketName = "test-bucket-9bc24fe0-77a0-4fb2-9b15-5e6f66972c6e",
        Location = "US-CENTRAL1",
        StorageClass = "STANDARD"
    };
    private readonly string _path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../credentials.json");
    private static Input _input;
    private static string _credentialsJson = "";
    private static string _directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData");
    private static string _pattern = "test.txt";
    private static string _contentType = "text/plain";

    [SetUp]
    public async Task Setup()
    {
        var base64EncodedBytes = Convert.FromBase64String(_credentialsBase64);
        _credentialsJson = Encoding.ASCII.GetString(base64EncodedBytes);
        File.WriteAllText(_path, _credentialsJson);

        _input = new Input
        {
            BucketName = _details.BucketName,
            ProjectId = _details.ProjectId,
            Directory = _directory,
            Pattern = _pattern,
            ContentType = _contentType,
            CredentialFilePath = _path,
            CredentialJson = ""
        };

        var files = new string[]
        {
            "_test.txt",
            "2t.txt",
            "pref_test.txt",
            "pro_test.txt",
            "pro_tet.txt",
            "prof_test.txt",
            "test.xml",
            "test.txt",
            "test.foo",
            "test1234.txt",
            "test6789.txt"
        };
        Directory.CreateDirectory(_directory);
        await UploadTestFilesAsync(_credentialsJson, _details, files, _directory);
    }

    [TearDown]
    public async Task Teardown()
    {
        using var client = await StorageClient.CreateAsync(GoogleCredential.FromFile(_path));
        foreach (var file in client.ListObjects(_details.BucketName))
            client.DeleteObject(file);
        client.DeleteBucket(_details.BucketName, null);

        File.Delete(_path);
        Directory.Delete(_directory, true);
    }

    [Test]
    public async Task DownloadObject()
    {
        var result = await GoogleCloudStorage.DownloadObject(_input, default);
        Assert.AreEqual(1, result.Count);
    }

    [Test]
    public async Task DownloadObject_CredentialsFromJsonString()
    {
        _input.CredentialFilePath = string.Empty;
        _input.CredentialJson = _credentialsJson;
        var result = await GoogleCloudStorage.DownloadObject(_input, default);
        Assert.AreEqual(1, result.Count);
    }

    [Test]
    public async Task DownloadObject_FileMask()
    {
        _input.Pattern = "<regex>^(?!prof).*_test.txt";
        var result = await GoogleCloudStorage.DownloadObject(_input, default);
        Assert.AreEqual(3, result.Count);

        _input.Pattern = @"test\d{4,4}.txt";
        result = await GoogleCloudStorage.DownloadObject(_input, default);
        Assert.AreEqual(2, result.Count);

        _input.Pattern = "test.[^t][^x][^t]";
        result = await GoogleCloudStorage.DownloadObject(_input, default);
        Assert.AreEqual(2, result.Count);

        _input.Pattern = "test.(txt|xml)";
        result = await GoogleCloudStorage.DownloadObject(_input, default);
        Assert.AreEqual(2, result.Count);

        _input.Pattern = "*test.txt";
        result = await GoogleCloudStorage.DownloadObject(_input, default);
        Assert.AreEqual(5, result.Count);
    }

    private static async Task UploadTestFilesAsync(string credentialsJson, dynamic details, string[] files, string path)
    {
        using var storage = await StorageClient.CreateAsync(GoogleCredential.FromJson(credentialsJson));
        var bucket = storage.ListBuckets((string)details.ProjectId, null).FirstOrDefault(n => n.Name.Equals(details.BucketName));
        if (bucket == null)
        {
            var newBucket = new Bucket
            {
                Name = details.BucketName,
                Location = details.Location,
                StorageClass = details.StorageClass,
            };
            _ = await storage.CreateBucketAsync(details.ProjectId, newBucket, null, new CancellationToken());
        }

        foreach (var file in files)
        {
            var fullPath = Path.Combine(path, file);
            File.WriteAllText(fullPath, "This is a test file");
            await storage.UploadObjectAsync(details.BucketName, file, "text/plain", new MemoryStream(File.ReadAllBytes(fullPath)));
        }
    }


}



