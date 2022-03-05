using ChatAppClient.Models;
using Microsoft.AspNetCore.SignalR;

namespace ChatAppServer.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessageAsync(Message message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
