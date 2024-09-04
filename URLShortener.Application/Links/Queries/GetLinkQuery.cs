using FluentValidation;
using URLShortener.Application.Shared.Requests;
using URLShortener.Application.Shared.Validations;

namespace URLShortener.Application.Links.Queries;

public class GetLinkQuery : Request
{
    public string Id { get; set; }

    public GetLinkQuery(string id)
    {
        Id = id;
        Validate(new GetLinkQueryValidator());
    }
}

public class GetLinkQueryValidator : Validator<GetLinkQuery>
{
    public GetLinkQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .Length(10);
    }
}
