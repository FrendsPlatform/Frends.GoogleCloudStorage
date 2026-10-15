using System;
using System.Threading;
using System.Threading.Tasks;
using Frends.GoogleCloudStorage.DeleteObject.Definitions;
using Frends.GoogleCloudStorage.DeleteObject.Enums;
using NUnit.Framework;

namespace Frends.GoogleCloudStorage.DeleteObject.Tests;

[TestFixture]
internal class ErrorHandlerTest
{
    private const string CustomErrorMessage = "CustomErrorMessage";

    private static Input InvalidInput() => new()
    {
        BucketName = "your-unique-bucket-name",
        Pattern = "*.txt",
        CredentialJson = string.Empty,
        CredentialFilePath = string.Empty,
        CredentialsType = CredentialsType.Json,
    };

    [Test]
    public void Should_Throw_Error_When_ThrowErrorOnFailure_Is_True()
    {
        var ex = Assert.ThrowsAsync<System.ComponentModel.DataAnnotations.ValidationException>(async () =>
            await GoogleCloudStorage.DeleteObject(InvalidInput(), new Options(), CancellationToken.None));
        Assert.That(ex.Message, Does.Contain("CredentialJson is required."));
    }

    [Test]
    public async Task Should_Return_Failed_Result_When_ThrowErrorOnFailure_Is_False()
    {
        var options = new Options { ThrowErrorOnFailure = false };
        var result = await GoogleCloudStorage.DeleteObject(InvalidInput(), options, CancellationToken.None);
        Assert.That(result.Success, Is.False);
        Assert.That(result.Error.Message, Does.Contain("CredentialJson is required."));
    }

    [Test]
    public async Task Should_Require_CredentialFilePath_When_FilePath_Is_Selected()
    {
        var input = InvalidInput();
        input.CredentialsType = CredentialsType.FilePath;
        input.CredentialJson = "unused";

        var result = await GoogleCloudStorage.DeleteObject(input, new Options { ThrowErrorOnFailure = false }, CancellationToken.None);

        Assert.That(result.Success, Is.False);
        Assert.That(result.Error.Message, Does.Contain("CredentialFilePath is required."));
    }

    [Test]
    public void Should_Use_Custom_ErrorMessageOnFailure()
    {
        var options = new Options { ErrorMessageOnFailure = CustomErrorMessage };
        var ex = Assert.ThrowsAsync<Exception>(async () =>
            await GoogleCloudStorage.DeleteObject(InvalidInput(), options, CancellationToken.None));
        Assert.That(ex.Message, Contains.Substring(CustomErrorMessage));
    }
}
