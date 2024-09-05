using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using URLShortener.Application.Shared.Requests;
using URLShortener.Domain.Shared.Notifications;
namespace URLShortener.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
public abstract class BaseApiController(NotificationContext notificationContext, IMediator mediator) : Controller
{
    private readonly NotificationContext _notificationContext = notificationContext;

    protected readonly IMediator _mediator = mediator;

    protected IActionResult CustomOKResponse(object result)
    {
        if (_notificationContext.HasNotifications)
        {
            return BadRequest(new ProblemDetails
            {
                Title = "Error",
                Detail = JsonSerializer.Serialize(_notificationContext.Notifications),
                Status = (int)HttpStatusCode.BadRequest,
                Instance = HttpContext.Request.Path,
                Type = "https://tools.ietf.org/html/rfc9110#section-15.5.1"
            });
        }

        if (result == null)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    protected IActionResult CustomRedirectResponse(RequestResult result)
    {
        if (_notificationContext.HasNotifications)
        {
            return BadRequest(new ProblemDetails
            {
                Title = "Error",
                Detail = JsonSerializer.Serialize(_notificationContext.Notifications),
                Status = (int)HttpStatusCode.BadRequest,
                Instance = HttpContext.Request.Path,
                Type = "https://tools.ietf.org/html/rfc9110#section-15.5.1"
            });
        }

        if (result == null)
        {
            return new NotFoundResult();
        }

        return Redirect(result.Value.ToString());
    }

    protected IActionResult CustomCreatedResponse(RequestResult result, string routeName)
    {
        if (_notificationContext.HasNotifications)
        {
            return BadRequest(new ProblemDetails
            {
                Title = "Error",
                Detail = JsonSerializer.Serialize(_notificationContext.Notifications),
                Status = (int)HttpStatusCode.BadRequest,
                Instance = HttpContext.Request.Path,
                Type = "https://tools.ietf.org/html/rfc9110#section-15.5.1"
            });
        }

        if (result == null || string.IsNullOrWhiteSpace(routeName))
        {
            return BadRequest();
        }

        var response = new
        {
            Link = Url.Link(routeName, new { id = result.Value })
        };

        return CreatedAtRoute(routeName, new
        {
            id = result.Value
        }, response);
    }
}
