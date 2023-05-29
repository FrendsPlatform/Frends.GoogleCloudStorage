using NUnit.Framework;
using System;
using System.Threading.Tasks;
using System.IO;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Frends.GoogleCloudStorage.CreateBucket.Definitions;
using Frends.GoogleCloudStorage.CreateBucket.Enums;

namespace Frends.GoogleCloudStorage.CreateBucket.Tests;

[TestFixture]
class UnitTests
{
    //private readonly string _credentialsJson = Environment.GetEnvironmentVariable("Frends_GoogleCloudStorage_CredJson");
    private readonly string _projectId = "instant-stone-387712";
    private readonly string _bucketName = "test-bucket";
    private readonly string _location = "US-CENTRAL1";
    private readonly StorageClassType _storageClass = StorageClassType.STANDARD;
    private readonly string _path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../credentials.json");
    private static Input _input;

    [SetUp]
    public void Setup()
    {
        //File.WriteAllText(_path, _credentialsJson);
        _input = new Input()
        {
            BucketName = _bucketName,
            Location = _location,
            StorageClass = _storageClass,
            ProjectId = _projectId,
            CredentialFilePath = _path,
            CredentialJson = "",
            AddGuidToBucketName = true
        };
    }

    [TearDown]
    public async Task Teardown()
    {
        //File.Delete(_path);

        using var client = await StorageClient.CreateAsync(GoogleCredential.FromFile(_path));
        var buckets = client.ListBuckets(_projectId);
        foreach (var bucket in buckets)
            client.DeleteBucket(bucket.Name);
    }

    [Test]
    public async Task CreateBucket()
    {
        var result = await GoogleCloudStorage.CreateBucket(_input, default);
        Assert.IsNotNull(result);
        var name = result.BucketName;
        Assert.AreEqual(name, name);
    } 
}



