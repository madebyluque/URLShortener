using Moq;
using URLShortener.Application.Links.Handlers;
using URLShortener.Application.Links.Queries;
using URLShortener.Domain.Cache;
using URLShortener.Domain.Entities.Links;
using URLShortener.Domain.Entities.Links.Cache;
using URLShortener.Domain.Entities.Links.Repositories;
using URLShortener.Domain.Shared.Notifications;

namespace URLShortener.UnitTests.Links.Application.Handlers;

public class GetLinkQueryHandlerTests
{
    private readonly Mock<ILinksRepository> _linksRepository;
    private readonly Mock<ICacheService> _cacheService;
    private readonly Mock<NotificationContext> _notificationContext;
    private readonly GetLinkQueryHandler _handler;

    public GetLinkQueryHandlerTests()
    {
        _linksRepository = new Mock<ILinksRepository>();
        _cacheService = new Mock<ICacheService>();
        _notificationContext = new Mock<NotificationContext>();

        _handler = new GetLinkQueryHandler(_linksRepository.Object, _cacheService.Object, _notificationContext.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnNull_WhenRequestIsInvalid()
    {
        // Arrange
        var query = new GetLinkQuery(null);

        // Act
        var result = await _handler.Handle(query, It.IsAny<CancellationToken>());

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task Handle_ShouldReturnLinkAddress_WhenLinkIsFoundInCache()
    {
        // Arrange
        var link = new Link(new Uri("https://test.test"));
        var query = new GetLinkQuery(link.Id);
        _cacheService.Setup(x => x.GetAsync<Link>(It.Is<LinkCacheKey>(x => x.Key == $"{nameof(Link)}-{link.Id}"))).ReturnsAsync(link);

        // Act
        var result = await _handler.Handle(query, It.IsAny<CancellationToken>());

        // Assert
        Assert.Equal(link.Address, result.Value);
        _cacheService.Verify(x => x.GetAsync<Link>(It.Is<LinkCacheKey>(x => x.Key == $"{nameof(Link)}-{link.Id}")), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnNull_WhenLinkIsNotFound()
    {
        // Arrange
        var linkId = "0123456789";
        var query = new GetLinkQuery(linkId);

        // Act
        var result = await _handler.Handle(query, It.IsAny<CancellationToken>());

        // Assert
        Assert.Null(result);
        _linksRepository.Verify(x => x.GetLinkById(linkId), Times.Once);
        _cacheService.Verify(x => x.GetAsync<Link>(It.Is<LinkCacheKey>(x => x.Key == $"{nameof(Link)}-{linkId}")), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnLinkAddress()
    {
        // Arrange
        var link = new Link(new Uri("https://test.test"));
        var query = new GetLinkQuery(link.Id);
        _linksRepository.Setup(x => x.GetLinkById(link.Id)).ReturnsAsync(link);

        // Act
        var result = await _handler.Handle(query, It.IsAny<CancellationToken>());

        // Assert
        Assert.Equal(link.Address, result.Value);
        _cacheService.Verify(x => x.GetAsync<Link>(It.Is<LinkCacheKey>(x => x.Key == $"{nameof(Link)}-{link.Id}")), Times.Once);
        _linksRepository.Verify(x => x.GetLinkById(link.Id), Times.Once);
    }
}
