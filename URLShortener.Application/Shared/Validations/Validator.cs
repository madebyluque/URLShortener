using FluentValidation;
using URLShortener.Application.Shared.Requests;

namespace URLShortener.Application.Shared.Validations;

public abstract class Validator<T> : AbstractValidator<T> where T : Request
{
}
