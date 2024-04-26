namespace DroneManagement.OutboxPattern;

public class OrderProcessor(IOrderRepository orderRepository, IOrderNotifier notifier)
{
    public async Task PlaceOrder(Order order)
    {
        await orderRepository.Save(order);
        await notifier.Notify(order);
    }
}

public interface IOrderNotifier
{
    Task Notify(Order order);
}

public interface IOrderRepository
{
    Task Save(Order order);
}

public class Order(Guid id, string customerName, string address, decimal totalAmount)
{
    public Guid Id { get; private set; } = id;
    public string CustomerName { get; private set; } = customerName;
    public string Address { get; private set; } = address;
    public decimal TotalAmount { get; private set; } = totalAmount;
}