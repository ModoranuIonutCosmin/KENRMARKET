using Microsoft.AspNetCore.SignalR;

namespace Order.SignalR.Hubs;

public class OrdersHub : Hub
{
    //TODO: FIX
    // public async Task AnnounceCustomerOfOrderStatusUpdate(Guid customerId, Domain.Entities.Order order)
    // {
    //     
    //     var deserializationOptions = new JsonSerializerOptions
    //     {
    //         PropertyNameCaseInsensitive = true
    //     };
    //
    //     var orderAsString = JsonSerializer.Serialize<Domain.Entities.Order>(order, deserializationOptions);
    //
    //     await Clients.User(customerId.ToString()).SendAsync("ReceiveOrdersUpdate", orderAsString);
    // }
}