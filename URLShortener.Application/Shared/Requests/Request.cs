using MediatR;
using URLShortener.Application.Shared.Validations;
using URLShortener.Domain.Shared.Notifications;

namespace URLShortener.Application.Shared.Requests;


public abstract class Request : IRequest<RequestResult>
{
    public bool IsValid { get; set; } = false;
    public List<Notification> Notifications { get; set; } = new List<Notification>();

    protected void Validate<T>(Validator<T> validator) where T : Request
    {
        var validationResult = validator.Validate((T)this);
        IsValid = validationResult.IsValid;
        Notifications = validationResult.Errors.Select(x => new Notification(x.PropertyName, x.ErrorMessage)).ToList();
    }
}
