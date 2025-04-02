using Microsoft.AspNetCore.SignalR;

namespace AlphaWebApp.Hubs;

public class NotificationHub : Hub
{
    public async Task SendNotification(object notification)
    {
        await Clients.All.SendAsync("RecieveNotification", notification);
    }

    public async Task SendNotificationOnlyAdmin(object notification)
    {
        await Clients.Group("Admins").SendAsync("AdminRecieveNotification", notification);
    }
}
