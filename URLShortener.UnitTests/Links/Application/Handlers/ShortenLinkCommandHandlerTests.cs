using Moq;
using URLShortener.Application.Links.Commands;
using URLShortener.Application.Links.Handlers;
using URLShortener.Domain.Entities.Links;
using URLShortener.Domain.Entities.Links.Repositories;
using URLShortener.Domain.Shared.Notifications;

namespace URLShortener.UnitTests.Links.Application.Handlers;

public class ShortenLinkCommandHandlerTests
{
    private readonly Mock<ILinksRepository> _linksRepository;
    private readonly Mock<NotificationContext> _notificationContext;

    private readonly ShortenLinkCommandHandler _handler;

    public ShortenLinkCommandHandlerTests()
    {
        _linksRepository = new Mock<ILinksRepository>();
        _notificationContext = new Mock<NotificationContext>();

        _handler = new ShortenLinkCommandHandler(_linksRepository.Object, _notificationContext.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnNull_WhenRequestIsInvalid()
    {
        // Arrange
        var command = new ShortenLinkCommand(null);

        // Act
        var result = await _handler.Handle(command, It.IsAny<CancellationToken>());

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task Handle_ShouldReturnExistentLinkId_WhenLinkIsFoundInRepository()
    {
        // Arrange
        var address = new Uri("https://test.test");
        var link = new Link(address);
        _linksRepository.Setup(x => x.GetLinkByAddress(address)).ReturnsAsync(link);

        var command = new ShortenLinkCommand(address);

        // Act
        var result = await _handler.Handle(command, It.IsAny<CancellationToken>());

        // Assert
        Assert.Equal(link.Id, result.Value);
        _linksRepository.Verify(x => x.CreateLink(It.IsAny<Link>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldReturnExistentLinkId()
    {
        // Arrange
        var address = new Uri("https://test.test");
        var link = new Link(address);
        _linksRepository.Setup(x => x.GetLinkByAddress(address)).ReturnsAsync(default(Link));

        var command = new ShortenLinkCommand(address);

        // Act
        var result = await _handler.Handle(command, It.IsAny<CancellationToken>());

        // Assert
        Assert.NotNull(result);
        _linksRepository.Verify(x => x.CreateLink(It.IsAny<Link>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
