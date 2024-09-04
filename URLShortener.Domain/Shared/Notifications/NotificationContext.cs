namespace URLShortener.Domain.Shared.Notifications;

public class NotificationContext
{
    private readonly List<Notification> _notifications;
    public IReadOnlyCollection<Notification> Notifications => _notifications;
    public bool HasNotifications => _notifications.Count != 0; // VS recommended this instead of "Count" for performance improvement

    public NotificationContext()
    {
        _notifications = new List<Notification>();
    }

    public void AddNotification(string key, string message)
    {
        _notifications.Add(new Notification(key, message));
    }

    public void AddNotification(Notification notification)
    {
        _notifications.Add(notification);
    }

    public void AddNotifications(IList<Notification> notifications)
    {
        _notifications.AddRange(notifications);
    }

    public void AddNotifications(ICollection<Notification> notifications)
    {
        _notifications.AddRange(notifications);
    }
}