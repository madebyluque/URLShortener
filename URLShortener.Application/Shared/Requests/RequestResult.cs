namespace URLShortener.Application.Shared.Requests;

public class RequestResult(object value = null)
{
    public object Value { get; set; } = value;
}

