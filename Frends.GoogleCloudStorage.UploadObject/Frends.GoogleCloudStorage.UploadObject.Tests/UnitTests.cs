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
using Frends.GoogleCloudStorage.UploadObject.Definitions;

namespace Frends.GoogleCloudStorage.UploadObject.Tests;

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
    private static string _pattern = "*.txt";
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

        await CreateBucket(_credentialsJson, _details);
        Directory.CreateDirectory(_directory);
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
    public async Task UploadObject()
    {
        File.WriteAllText(Path.Combine(_directory, "test.txt"), "This is a test file.");
        var result = await GoogleCloudStorage.UploadObject(_input, default);
        Assert.AreEqual(1, result.Count);
    }

    [Test]
    public async Task UploadObject_CredentialsFromJsonString()
    {
        File.WriteAllText(Path.Combine(_directory, "test.txt"), "This is a test file.");
        _input.CredentialFilePath = string.Empty;
        _input.CredentialJson = _credentialsJson;
        var result = await GoogleCloudStorage.UploadObject(_input, default);
        Assert.AreEqual(1, result.Count);
    }

    [Test]
    public async Task UploadObject_FileMask()
    {
        var files = new string[]
        {
            "_test.txt",
            "2t.txt",
            "pref_test.txt",
            "pro_test.txt",
            "pro_tet.txt",
            "prof_test.txt"
        };
        foreach (var file in files)
        {
            File.WriteAllText(Path.Combine(_directory, file), "This is a test file.");
        }
        _input.Pattern = "*.txt";
        var result = await GoogleCloudStorage.UploadObject(_input, default);
        Assert.AreEqual(6, result.Count);
    }

    private static async Task CreateBucket(string credentialsJson, dynamic details)
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
    }
}



