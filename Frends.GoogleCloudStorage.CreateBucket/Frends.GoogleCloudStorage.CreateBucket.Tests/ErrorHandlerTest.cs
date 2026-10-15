using System;
using System.Threading;
using System.Threading.Tasks;
using Frends.GoogleCloudStorage.CreateBucket.Definitions;
using Frends.GoogleCloudStorage.CreateBucket.Enums;
using NUnit.Framework;

namespace Frends.GoogleCloudStorage.CreateBucket.Tests;

[TestFixture]
class ErrorHandlerTest
{
    private const string CustomErrorMessage = "CustomErrorMessage";

    private static Input InvalidInput() => new()
    {
        BucketName = "test-bucket",
        Location = "US-CENTRAL1",
        StorageClass = StorageClassType.STANDARD,
        ProjectId = "invalid-project",
        CredentialJson = "invalid json",
    };

    [Test]
    public void Should_Throw_Error_When_ThrowErrorOnFailure_Is_True()
    {
        var ex = Assert.CatchAsync<Exception>(async () =>
            await GoogleCloudStorage.CreateBucket(InvalidInput(), new Options(), CancellationToken.None));
        Assert.That(ex, Is.Not.Null);
    }

    [Test]
    public async Task Should_Return_Failed_Result_When_ThrowErrorOnFailure_Is_False()
    {
        var options = new Options { ThrowErrorOnFailure = false };
        var result = await GoogleCloudStorage.CreateBucket(InvalidInput(), options, CancellationToken.None);
        Assert.That(result.Success, Is.False);
        Assert.That(result.Error, Is.Not.Null);
    }

    [Test]
    public void Should_Use_Custom_ErrorMessageOnFailure()
    {
        var options = new Options { ErrorMessageOnFailure = CustomErrorMessage };
        var ex = Assert.ThrowsAsync<Exception>(async () =>
            await GoogleCloudStorage.CreateBucket(InvalidInput(), options, CancellationToken.None));
        Assert.That(ex.Message, Does.Contain(CustomErrorMessage));
    }
}
