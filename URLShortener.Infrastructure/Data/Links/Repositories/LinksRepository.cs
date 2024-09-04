using MediatR;
using Microsoft.EntityFrameworkCore;
using URLShortener.Domain.Entities.Links;
using URLShortener.Domain.Entities.Links.Repositories;
using URLShortener.Infrastructure.Data.Contexts;
using URLShortener.Infrastructure.Data.Extensions;

namespace URLShortener.Infrastructure.Data.Links.Repositories;

public class LinksRepository(UriShortenerContext context, IMediator mediator) : ILinksRepository
{
    private readonly UriShortenerContext _context = context;
    private readonly IMediator _mediator = mediator;

    public async Task<Link> GetLinkByAddress(Uri address)
    {
        return await _context.Links.FirstOrDefaultAsync(x => x.Address == address);
    }

    public async Task<bool> CreateLink(Link link, CancellationToken cancellationToken)
    {
        await _context.Links.AddAsync(link);
        return await SaveChanges(cancellationToken) > 0;
    }

    public async Task<bool> DeleteLink(string id, CancellationToken cancellationToken)
    {
        var link = await _context.Links.FirstOrDefaultAsync(x => x.Id == id);
        _context.Links.Remove(link);
        return await SaveChanges(cancellationToken) > 0;
    }

    public async Task<Link> GetLinkById(string id)
    {
        return await _context.Links.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> UpdateLink(Link link, CancellationToken cancellationToken)
    {
        var linkToUpdate = await GetLinkById(link.Id);

        if (linkToUpdate == null)
        {
            return false;
        }

        _context.Update(link);

        return await SaveChanges(cancellationToken) > 0;
    }

    private async Task<int> SaveChanges(CancellationToken cancellationToken)
    {
        await _mediator.DispatchDomainEvents(_context, cancellationToken);

        return await _context.SaveChangesAsync(cancellationToken);
    }
}
