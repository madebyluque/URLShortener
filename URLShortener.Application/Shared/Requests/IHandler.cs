using MediatR;

namespace URLShortener.Application.Shared.Requests;

public interface IHandler<T> : IRequestHandler<T, RequestResult> where T : IRequest<RequestResult>
{
}