using System;
using System.Threading;
using System.Threading.Tasks;
using Frends.GoogleCloudStorage.DownloadObject.Definitions;
using Frends.GoogleCloudStorage.DownloadObject.Enums;
using NUnit.Framework;

namespace Frends.GoogleCloudStorage.DownloadObject.Tests;

[TestFixture]
class ErrorHandlerTest
{
    private const string CustomErrorMessage = "CustomErrorMessage";

    private static Input InvalidInput() => new()
    {
        BucketName = string.Empty,
        Directory = "TestData",
        Pattern = "*.txt",
        CredentialsType = CredentialType.Json,
        CredentialJson = "{}",
    };

    [Test]
    public void Should_Throw_Error_When_ThrowErrorOnFailure_Is_True()
    {
        var ex = Assert.ThrowsAsync<System.ComponentModel.DataAnnotations.ValidationException>(async () =>
            await GoogleCloudStorage.DownloadObject(InvalidInput(), new Options(), CancellationToken.None));
        Assert.That(ex, Is.Not.Null);
    }

    [Test]
    public async Task Should_Return_Failed_Result_When_ThrowErrorOnFailure_Is_False()
    {
        var options = new Options { ThrowErrorOnFailure = false };
        var result = await GoogleCloudStorage.DownloadObject(InvalidInput(), options, CancellationToken.None);
        Assert.That(result.Success, Is.False);
        Assert.That(result.Error, Is.Not.Null);
    }

    [Test]
    public void Should_Use_Custom_ErrorMessageOnFailure()
    {
        var options = new Options { ErrorMessageOnFailure = CustomErrorMessage };
        var ex = Assert.ThrowsAsync<Exception>(async () =>
            await GoogleCloudStorage.DownloadObject(InvalidInput(), options, CancellationToken.None));
        Assert.That(ex.Message, Contains.Substring(CustomErrorMessage));
    }

    [Test]
    public void Should_Require_Selected_Credentials_Input()
    {
        var input = new Input
        {
            BucketName = "bucket",
            Directory = "TestData",
            Pattern = "*.txt",
            CredentialsType = CredentialType.FilePath,
        };

        var ex = Assert.ThrowsAsync<System.ComponentModel.DataAnnotations.ValidationException>(async () =>
            await GoogleCloudStorage.DownloadObject(input, new Options(), CancellationToken.None));
        Assert.That(ex.Message, Contains.Substring("CredentialFilePath is required"));
    }

    [Test]
    public async Task Should_Validate_Options()
    {
        var options = new Options
        {
            ErrorMessageOnFailure = new string('x', 1025),
            ThrowErrorOnFailure = false,
        };
        var result = await GoogleCloudStorage.DownloadObject(InvalidInput(), options, CancellationToken.None);
        Assert.That(result.Success, Is.False);
        Assert.That(result.Error.Message, Contains.Substring("ErrorMessageOnFailure cannot exceed 1024 characters"));
    }
}
