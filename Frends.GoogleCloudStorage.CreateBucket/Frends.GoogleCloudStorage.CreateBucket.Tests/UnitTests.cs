using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Frends.GoogleCloudStorage.CreateBucket.Definitions;
using Frends.GoogleCloudStorage.CreateBucket.Enums;

namespace Frends.GoogleCloudStorage.CreateBucket.Tests;

[TestFixture]
class UnitTests
{
    private readonly string _credentialsBase64 = Environment.GetEnvironmentVariable("Frends_GoogleCloudStorage_CredJson");
    private readonly string _projectId = "instant-stone-387712";
    private string _bucketName;
    private readonly List<string> _createdBuckets = new();
    private readonly string _location = "US-CENTRAL1";
    private readonly StorageClassType _storageClass = StorageClassType.STANDARD;
    private readonly string _path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../credentials.json");
    private static Input _input;
    private static Options _options;
    private static string _credentialsJson = "";

    [SetUp]
    public void Setup()
    {
        _bucketName = $"test-bucket-{Guid.NewGuid()}";
        _createdBuckets.Clear();
        var base64EncodedBytes = Convert.FromBase64String(_credentialsBase64);
        _credentialsJson = Encoding.ASCII.GetString(base64EncodedBytes);
        File.WriteAllText(_path, _credentialsJson);
        _options = new Options();
        _input = new Input()
        {
            BucketName = _bucketName,
            Location = _location,
            StorageClass = _storageClass,
            ProjectId = _projectId,
            CredentialsType = CredentialType.FilePath,
            CredentialFilePath = _path,
            CredentialJson = "",
            AddGuidToBucketName = false
        };
    }

    [TearDown]
    public async Task Teardown()
    {
        using var client = await StorageClient.CreateAsync(GoogleCredential.FromFile(_path));
        foreach (var bucket in _createdBuckets)
        {
            try
            {
                client.DeleteBucket(bucket, new DeleteBucketOptions { DeleteObjects = true });
            }
            catch (Google.GoogleApiException ex) when (ex.HttpStatusCode == System.Net.HttpStatusCode.NotFound)
            {
            }
        }
        File.Delete(_path);
    }

    [Test]
    public async Task CreateBucket()
    {
        var result = await GoogleCloudStorage.CreateBucket(_input, _options, default);
        _createdBuckets.Add(result.BucketName);
        Assert.IsNotNull(result);
        Assert.AreEqual(_bucketName, result.BucketName);
    }

    [Test]
    public async Task CreateBucket_CredentialsFromJsonString()
    {
        _input.CredentialFilePath = string.Empty;
        _input.CredentialsType = CredentialType.Json;
        _input.CredentialJson = _credentialsJson;
        var result = await GoogleCloudStorage.CreateBucket(_input, _options, default);
        _createdBuckets.Add(result.BucketName);
        Assert.AreEqual(_bucketName, result.BucketName);
    }

    [Test]
    public async Task CreateBucket_EmptyBucketName()
    {
        _input.BucketName = string.Empty;
        var result = await GoogleCloudStorage.CreateBucket(_input, _options, default);
        _createdBuckets.Add(result.BucketName);
        Assert.IsTrue(result.Success);
    }

    [Test]
    public async Task CreateBucket_AddGuidToBucketName()
    {
        _input.BucketName = "test-bucket";
        _input.AddGuidToBucketName = true;
        var result = await GoogleCloudStorage.CreateBucket(_input, _options, default);
        _createdBuckets.Add(result.BucketName);
        Assert.IsTrue(result.Success);
    }

}

[TestFixture]
class InputValidationTests
{
    [Test]
    public void CreateBucket_RequiresProjectId()
    {
        var input = new Input
        {
            ProjectId = string.Empty,
            Location = "US-CENTRAL1",
            CredentialsType = CredentialType.FilePath,
            CredentialFilePath = "credentials.json"
        };

        Assert.ThrowsAsync<ValidationException>(async () =>
            await GoogleCloudStorage.CreateBucket(input, new Options(), default));
    }

    [Test]
    public void CreateBucket_RequiresSelectedCredential()
    {
        var input = new Input
        {
            ProjectId = "project-id",
            Location = "US-CENTRAL1",
            CredentialsType = CredentialType.FilePath
        };

        Assert.ThrowsAsync<ValidationException>(async () =>
            await GoogleCloudStorage.CreateBucket(input, new Options(), default));
    }
}
