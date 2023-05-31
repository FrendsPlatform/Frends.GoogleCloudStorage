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
using Frends.GoogleCloudStorage.DeleteBucket.Definitions;

namespace Frends.GoogleCloudStorage.DeleteBucket.Tests;

[TestFixture]
class UnitTests
{
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
            DeleteObjects = true,
            ThrowIfNoBucketFound = true,
            CredentialFilePath = _path,
            CredentialJson = ""
        };

        await CreateBucket(_credentialsJson, _details);
    }

    [TearDown]
    public void Teardown()
    {
        File.Delete(_path);
    }

    [Test]
    public async Task DeleteBucket()
    {
        var result = await GoogleCloudStorage.DeleteBucket(_input, default);
        Assert.IsTrue(result.Success);
    }

    [Test]
    public async Task CreateBucket_CredentialsFromJsonString()
    {
        _input.CredentialFilePath = string.Empty;
        _input.CredentialJson = _credentialsJson;
        var result = await GoogleCloudStorage.DeleteBucket(_input, default);
        Assert.IsTrue(result.Success);
    }

    [Test]
    public async Task CreateBucket_EmptyBucketNameShouldThrow()
    {
        _ = await GoogleCloudStorage.DeleteBucket(_input, default);
        _input.BucketName = string.Empty;
        var ex = Assert.ThrowsAsync<ArgumentException>(() => GoogleCloudStorage.DeleteBucket(_input, default));
        Assert.AreEqual("Bucket name is missing.", ex.Message);
    }

    [Test]
    public async Task CreateBucket_NoBucketToDeleteShouldNotThrow()
    {
        _ = await GoogleCloudStorage.DeleteBucket(_input, default);
        _input.ThrowIfNoBucketFound = false;
        var result = await GoogleCloudStorage.DeleteBucket(_input, default);
        Assert.IsFalse(result.Success);
    }

    [Test]
    public async Task CreateBucket_NoBucketToDeleteShouldThrow()
    {
        _ = await GoogleCloudStorage.DeleteBucket(_input, default);
        var ex = Assert.ThrowsAsync<ArgumentException>(() => GoogleCloudStorage.DeleteBucket(_input, default));
        Assert.AreEqual("No bucket found.", ex.Message);
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



