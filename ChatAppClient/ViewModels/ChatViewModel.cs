using ChatAppClient.Commands;
using ChatAppClient.Models;
using ChatAppClient.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChatAppClient.ViewModels
{
    public class ChatViewModel : BaseViewModel
    {

        public ObservableCollection<Message> MyMessage { get; set; }
        public ObservableCollection<Message> OtherMessage { get; set; }

        private string _text;
        public string Text {
            get => _text;
            set
            {
                _text = value;
                OnPropertyChanged(nameof(Text));
            }
        }

        private string _errorMessage = string.Empty;
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
                OnPropertyChanged(nameof(HasErrorMessage));
            }
        }

        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);

        public ICommand SendMessageCommand { get; set; }

        public ChatViewModel(SignalRChatService chatService)
        {
            SendMessageCommand = new SendMessageCommand(this, chatService);
            Text = "new text";
            MyMessage = new(); //from database
            OtherMessage = new(); //db

            chatService.MessageReceived += ChatService_MessageReceived;
        }

        public static ChatViewModel CreateConnectionViewModel(SignalRChatService chatService)
        {
            var vm = new ChatViewModel(chatService);

            chatService.Connect().ContinueWith(task =>
            {
                if (task.Exception != null)
                    vm.ErrorMessage = "Check your network!";
            });

            return vm;
        }

        private void ChatService_MessageReceived(Message msg)
        {
            //solve sender problem
            MyMessage.Add(msg);
        }
    }
}
