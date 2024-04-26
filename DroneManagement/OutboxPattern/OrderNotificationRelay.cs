namespace DroneManagement.OutboxPattern;

public class OrderNotificationRelay(IOrderRepository orderRepository, IOrderNotifier orderNotifier)
{
    public async Task NotifyOrderPlacement()
    {
        var notification = await orderRepository.RetrieveOutstandingNotification();
        await orderNotifier.Notify(notification.Order);
    }
}