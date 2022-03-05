using ChatAppClient.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppClient.Services
{
    public class SignalRChatService
    {
        private readonly HubConnection _connection;

        public event Action<Message> MessageReceived;

        public SignalRChatService(HubConnection connection)
        {
            this._connection = connection;
            _connection.On<Message>("ReceiveMessage", (message) => MessageReceived?.Invoke(message));
        }

        public async Task Connect()
        {
            await _connection.StartAsync();
        }

        public async Task SendMessageAsync(Message message)
        {
            await _connection.SendAsync("SendMessageAsync", message);
        }
    }
}
