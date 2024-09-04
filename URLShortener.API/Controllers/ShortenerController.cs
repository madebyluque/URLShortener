using MediatR;
using Microsoft.AspNetCore.Mvc;
using URLShortener.API.Models;
using URLShortener.Application.Links.Commands;
using URLShortener.Application.Links.Queries;
using URLShortener.Domain.Shared.Notifications;

namespace URLShortener.API.Controllers;

[Route("[controller]")]
public class ShortenerController(NotificationContext notificationContext, IMediator mediator) : BaseApiController(notificationContext, mediator)
{
    [HttpPost]
    public async Task<IActionResult> Shorten([FromBody] ShortenLinkModel model)
    {
        var command = new ShortenLinkCommand(model.Address);
        var result = await _mediator.Send(command);
        return CustomCreatedResponse(result, nameof(GetUrl));
    }

    [HttpGet("/{id}", Name = nameof(GetUrl))]
    public async Task<IActionResult> GetUrl([FromRoute] string id)
    {
        var query = new GetLinkQuery(id);
        var result = await _mediator.Send(query);
        return CustomRedirectResponse(result.Value);
    }
}
