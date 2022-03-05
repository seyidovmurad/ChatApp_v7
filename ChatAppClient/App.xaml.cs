using ChatAppClient.Services;
using ChatAppClient.ViewModels;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ChatAppClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {

            HubConnection connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5187/chat")
                .Build();

            ChatViewModel chatViewModel = ChatViewModel.CreateConnectionViewModel(new SignalRChatService(connection));

            MainWindow window = new MainWindow
            {
                DataContext = new MainViewModel(chatViewModel)
            };
            window.Show();
        }
    }
}
