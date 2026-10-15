using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Frends.GoogleCloudStorage.UploadObject.Definitions;
using NUnit.Framework;

namespace Frends.GoogleCloudStorage.UploadObject.Tests;

[TestFixture]
internal class ErrorHandlerTests
{
    private const string CustomErrorMessage = "CustomErrorMessage";

    [Test]
    public void Should_Throw_Error_When_ThrowErrorOnFailure_Is_True()
    {
        var options = new Options();
        var ex = Assert.ThrowsAsync<ValidationException>(async () =>
            await GoogleCloudStorage.UploadObject(null, options, default));

        Assert.That(ex, Is.Not.Null);
    }

    [Test]
    public async Task Should_Return_Failed_Result_When_ThrowErrorOnFailure_Is_False()
    {
        var options = new Options { ThrowErrorOnFailure = false };
        var result = await GoogleCloudStorage.UploadObject(null, options, default);

        Assert.That(result.Success, Is.False);
        Assert.That(result.Error, Is.Not.Null);
    }

    [Test]
    public void Should_Use_Custom_ErrorMessageOnFailure()
    {
        var options = new Options { ErrorMessageOnFailure = CustomErrorMessage };
        var ex = Assert.ThrowsAsync<Exception>(async () =>
            await GoogleCloudStorage.UploadObject(null, options, default));

        Assert.That(ex, Is.Not.Null);
        Assert.That(ex.Message, Contains.Substring(CustomErrorMessage));
    }
}
