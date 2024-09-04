namespace URLShortener.Domain.Entities.Links.Repositories;

public interface ILinksRepository
{
    Task<Link> GetLinkById(string id);
    Task<bool> CreateLink(Link link, CancellationToken cancellationToken);
    Task<bool> DeleteLink(string id, CancellationToken cancellationToken);
    Task<bool> UpdateLink(Link link, CancellationToken cancellationToken);
    Task<Link> GetLinkByAddress(Uri address);
}
