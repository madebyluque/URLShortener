namespace URLShortener.Domain.Shared.Notifications;

public class Notification(string key, string value)
{
    public string Key { get; set; } = key;
    public string Value { get; set; } = value;
}
