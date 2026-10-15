using System;
using System.Threading.Tasks;
using Frends.GoogleCloudStorage.DeleteBucket.Definitions;
using NUnit.Framework;

namespace Frends.GoogleCloudStorage.DeleteBucket.Tests;

[TestFixture]
internal class ErrorHandlerTest
{
    private const string CustomErrorMessage = "Bucket deletion failed";

    [Test]
    public void Should_Throw_Error_When_ThrowErrorOnFailure_Is_True()
    {
        var exception = Assert.ThrowsAsync<ArgumentException>(async () =>
            await GoogleCloudStorage.DeleteBucket(new Input(), default, new Options()));
        Assert.That(exception, Is.Not.Null);
    }

    [Test]
    public async Task Should_Return_Failed_Result_When_ThrowErrorOnFailure_Is_False()
    {
        var options = new Options { ThrowErrorOnFailure = false };
        var result = await GoogleCloudStorage.DeleteBucket(new Input(), default, options);
        Assert.That(result.Success, Is.False);
        Assert.That(result.Error.Message, Is.EqualTo("Credentials are missing."));
    }

    [Test]
    public void Should_Use_Custom_ErrorMessageOnFailure()
    {
        var options = new Options { ErrorMessageOnFailure = CustomErrorMessage };
        var exception = Assert.ThrowsAsync<Exception>(async () =>
            await GoogleCloudStorage.DeleteBucket(new Input(), default, options));
        Assert.That(exception.Message, Does.Contain(CustomErrorMessage));
        Assert.That(exception.InnerException, Is.TypeOf<ArgumentException>());
    }
}
