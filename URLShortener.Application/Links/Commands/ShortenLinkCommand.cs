using FluentValidation;
using URLShortener.Application.Shared.Requests;
using URLShortener.Application.Shared.Validations;

namespace URLShortener.Application.Links.Commands;

public class ShortenLinkCommand : Request
{
    public Uri Address { get; set; }

    public ShortenLinkCommand(Uri address)
    {
        Address = address;

        Validate(new ShortenLinkCommandValidator());
    }
}

public class ShortenLinkCommandValidator : Validator<ShortenLinkCommand>
{
    public ShortenLinkCommandValidator()
    {
        RuleFor(x => x.Address)
            .NotEmpty();
    }
}
