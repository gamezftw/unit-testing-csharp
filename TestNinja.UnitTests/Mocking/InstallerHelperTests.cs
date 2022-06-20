using System.Net;

namespace TestNinja.UnitTests.Mocking;

public class InstallerHelperTests
{
    private Mock<IFileDownloader> _fileDownloader;
    private InstallerHelper _installerHelper;

    [SetUp]
    public void Setup()
    {
        _fileDownloader = new Mock<IFileDownloader>();
        _installerHelper = new InstallerHelper(_fileDownloader.Object);
    }

    [Test]
    public void DownloadInstaller_DownloadFails_ReturnFalse()
    {
        _fileDownloader.Setup(fd => 
            fd.DownloadFile(It.IsAny<string>(), It.IsAny<string>()))
            .Throws<WebException>();

        var result = _installerHelper.DownloadInstaller("customer", "installer");

        Assert.That(result, Is.False);
    }
    [Test]
    public void DownloadInstaller_DownloadSucceeds_ReturnTrue()
    {
        // _fileDownloader.Setup(fd => 
        //     fd.DownloadFile(It.IsAny<string>(), It.IsAny<string>()))
        //     .Throws<WebException>();

        var result = _installerHelper.DownloadInstaller("customer", "installer");

        Assert.That(result, Is.True);
    }
}