using Moq;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking;

[TestFixture]
public class VideoServiceTests
{
    private Mock<IFileReader> _fileReader;
    private VideoService _service;
    private Mock<IVideoRepository> _videoRepository;

    [SetUp]
    public void Setup()
    {
        _fileReader = new Mock<IFileReader>();
        _videoRepository = new Mock<IVideoRepository>();
        _service = new VideoService(_fileReader.Object, _videoRepository.Object);
    }

    [Test]
    public void ReadVideoTitle_EmptyFile_ReturnError()
    {
        _fileReader.Setup(fr => fr.Read("video.txt")).Returns("");

        var result = _service.ReadVideoTitle();

        Assert.That(result, Does.Contain("error").IgnoreCase);
    }

    [Test]
    public void GetUnprocessedVideos_AllVideosAreProcessed_RetrunAnEmptyString()
    {
        _videoRepository.Setup(vr => vr.GetUnprocessedVideos()).Returns(Enumerable.Empty<Video>());

        var result = _service.GetUnprocessedVideosAsCsv();

        Assert.That(result, Is.EqualTo(string.Empty));
    }
    [Test]
    public void GetUnprocessedVideos_SomeUnprocessedVideos_RetrunAStringWithIdOfUnprocessedVideos()
    {
        // _videoRepository.Setup(vr => vr.GetUnprocessedVideos()).Returns(Enumerable.Empty<Video>());
        _videoRepository.Setup(vr => vr.GetUnprocessedVideos()).Returns(new List<Video>
        {
             new Video(){Id = 1},
             new Video(){Id = 2},
             new Video(){Id = 3}
        });

        var result = _service.GetUnprocessedVideosAsCsv();

        Assert.That(result, Is.EqualTo("1,2,3"));
    }
}