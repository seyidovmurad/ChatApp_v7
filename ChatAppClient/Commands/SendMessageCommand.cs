using ChatAppClient.Models;
using ChatAppClient.Services;
using ChatAppClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChatAppClient.Commands
{
    public class SendMessageCommand : ICommand
    {
        private readonly ChatViewModel _chatViewModel;
        private readonly SignalRChatService _chatService;

        public SendMessageCommand(ChatViewModel chatViewModel, SignalRChatService chatService)
        {
            _chatViewModel = chatViewModel;
            _chatService = chatService;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public async void Execute(object? parameter)
        {
            try
            {
                await _chatService.SendMessageAsync(new Message
                {
                    Text = _chatViewModel.Text,
                    Time = DateTime.Now
                });
                _chatViewModel.ErrorMessage = string.Empty;
            }
            catch (Exception)
            {
                _chatViewModel.ErrorMessage = "Can't send message. Check your network!";
            }
        }
    }
}
