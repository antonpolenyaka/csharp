using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Windows;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace WpfApp7
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ConcurrentDictionary<string, long> _userNameToChatId;
        TelegramBotClient _botClient;
        private string _telegramTokenTedisNet = "1441964171:AAHfKUDbQz4y2qOJ9mIffA-thc4m5nxBW_k";

        public MainWindow()
        {
            InitializeComponent();
            _userNameToChatId = new ConcurrentDictionary<string, long>();
            _botClient = new TelegramBotClient(_telegramTokenTedisNet);
            _botClient.OnReceiveError += _botClient_OnReceiveError;
            _botClient.OnReceiveGeneralError += _botClient_OnReceiveGeneralError;
            _botClient.OnUpdate += _botClient_OnUpdate;
            _botClient.StartReceiving();
        }

        private void _botClient_OnUpdate(object sender, Telegram.Bot.Args.UpdateEventArgs e)
        {
            long chatId = e.Update.Message.Chat.Id;
            string firstName = e.Update.Message.Chat.FirstName;
            string lastName = e.Update.Message.Chat.LastName;
            ChatType chatType = e.Update.Message.Chat.Type;
            string username = e.Update.Message.Chat.Username ?? $"{firstName}{lastName}";
            Debug.WriteLine("user connected: " + username);
            _userNameToChatId.TryAdd(username, chatId);
            string messageToSend = $"TedisNet info:\n*Chat id:* {chatId}\n*First name:* {firstName}\n*Last name:* {lastName}\n*Chat type:* {chatType}\n*Username:* {username}";
            SendMessage(messageToSend, chatId);
        }

        private void _botClient_OnReceiveGeneralError(object sender, Telegram.Bot.Args.ReceiveGeneralErrorEventArgs e)
        {
            if (e.Exception != null)
            {
                Debug.WriteLine($"{e.Exception.Message} {e.Exception.StackTrace}");
            }
        }

        private void _botClient_OnReceiveError(object sender, Telegram.Bot.Args.ReceiveErrorEventArgs e)
        {
            if (e.ApiRequestException != null)
            {
                Debug.WriteLine($"{e.ApiRequestException.ErrorCode}: {e.ApiRequestException.Message}");
            }
        }

        public bool SendMessage(string messageToSend, long? chatId)
        {
            bool isOk = false;
            if (chatId.HasValue)
            {
                try
                {
                    Chat chat = new Chat()
                    {
                        Permissions = new ChatPermissions()
                        {
                            CanAddWebPagePreviews = false,
                            CanChangeInfo = false,
                            CanInviteUsers = false,
                            CanPinMessages = false,
                            CanSendMediaMessages = false,
                            CanSendMessages = true,
                            CanSendOtherMessages = false,
                            CanSendPolls = false
                        },
                        Id = chatId.Value,
                        Type = ChatType.Private
                    };
                    Message sended = _botClient.SendTextMessageAsync(chat, messageToSend, ParseMode.Markdown).GetAwaiter().GetResult();
                    isOk = sended != null;
                }
                catch (AggregateException ae)
                {
                    Debug.WriteLine($"{ae.Message}: {ae.StackTrace}");
                    isOk = false;
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"{e.Message}: {e.StackTrace}");
                    isOk = false;
                }
            }
            return isOk;
        }

        protected override void OnClosed(EventArgs e)
        {
            _botClient.StopReceiving();
            base.OnClosed(e);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            string messageToSend = $"{DateTime.Now} TedisNet: Interruptor abierto *52-1*, enviado desde `PC Principal`";
            string usernameDestination = txtUserName.Text;
            if (_userNameToChatId.TryGetValue(usernameDestination, out long chatId))
            {
                SendMessage(messageToSend, chatId);
            }
            else
            {
                MessageBox.Show("Write user, user write any in telegram!");
                txtUserName.Focus();
            }
        }
    }
}
