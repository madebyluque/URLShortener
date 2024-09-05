using Moq;
using URLShortener.Application.Links.Handlers.Events;
using URLShortener.Domain.Cache;
using URLShortener.Domain.Entities.Links;
using URLShortener.Domain.Entities.Links.Cache;
using URLShortener.Domain.Entities.Links.Events;

namespace URLShortener.UnitTests.Links.Application.Handlers;

public class LinkCreatedDomainEventHandlerTests
{
    private readonly Mock<ICacheService> _cacheService;
    private readonly LinkCreatedDomainEventHandler _handler;

    public LinkCreatedDomainEventHandlerTests()
    {
        _cacheService = new Mock<ICacheService>();
        _handler = new LinkCreatedDomainEventHandler(_cacheService.Object);
    }

    [Fact]
    public async Task Handle_ShouldNotCallCacheServiceSetAsync_WhenCacheIsNotConnected()
    {
        // Arrange
        _cacheService.Setup(x => x.IsConnected()).Returns(false);

        // Act
        await _handler.Handle(It.IsAny<LinkCreatedDomainEvent>(), It.IsAny<CancellationToken>());

        // Assert
        _cacheService.Verify(x => x.SetAsync(It.IsAny<LinkCacheEntry>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldCallCacheServiceSetAsync_WhenCacheIsConnected()
    {
        // Arrange
        _cacheService.Setup(x => x.IsConnected()).Returns(true);
        var domainEvent = new LinkCreatedDomainEvent(new Link(new Uri("https://tests.tests")));

        // Act
        await _handler.Handle(domainEvent, It.IsAny<CancellationToken>());

        // Assert
        _cacheService.Verify(x => x.SetAsync(It.IsAny<LinkCacheEntry>()), Times.Once);
    }
}
