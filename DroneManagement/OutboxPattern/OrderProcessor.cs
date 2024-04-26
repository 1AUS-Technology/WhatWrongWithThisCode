namespace DroneManagement.OutboxPattern;

public class OrderProcessor(IOrderRepository orderRepository)
{
    public async Task PlaceOrder(Order order)
    {
        OrderNotification orderNotification = new OrderNotification(order);
        await orderRepository.SaveOrderAndNotification(orderNotification);
    }
}

public class OrderNotification(Order order)
{
    public Guid OrderId { get; set; } = order.Id;
    public Order Order { get; set; } = order;
}

public interface IOrderNotifier
{
    Task Notify(Order order);
}

public interface IOrderRepository
{
    Task Save(Order order);
    Task SaveOrderAndNotification(OrderNotification orderNotification);
    Task<OrderNotification> RetrieveOutstandingNotification();
}

public class Order(Guid id, string customerName, string address, decimal totalAmount)
{
    public Guid Id { get; private set; } = id;
    public string CustomerName { get; private set; } = customerName;
    public string Address { get; private set; } = address;
    public decimal TotalAmount { get; private set; } = totalAmount;
}