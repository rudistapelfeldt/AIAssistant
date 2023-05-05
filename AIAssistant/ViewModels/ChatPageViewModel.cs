using System.Collections.ObjectModel;
using System.Text;
using AIAssistant.OpenAi.Interfaces;
using OpenAI_API.Chat;

namespace AIAssistant.ViewModels
{
    public class ChatPageViewModel : BindableBase, INavigationAware
    {
        #region private members
        IList<ChatMessage> _messageArray;
        readonly IOpenAiClient _openAiClient;

        readonly IPageDialogService _pageDialogService;
        #endregion

        #region Public members
        public string ChatInputText { get; set; }

        public string ConversationText { get; set; }

        public bool IsBusy { get; set; }

        public string NetworkAccess =>
            Connectivity.NetworkAccess.ToString();

        public string ConnectionProfiles
        {
            get
            {
                var profiles = string.Empty;
                foreach (var p in Connectivity.ConnectionProfiles)
                    profiles += "\n" + p.ToString();
                return profiles;
            }
        }

        public ObservableCollection<ChatMessage> ConversationList { get; set; }
        #endregion

        #region Commands
        public DelegateCommand SendChatInputCommand => new DelegateCommand(async () => await SendChatInput());

        public DelegateCommand NewChatCommand => new DelegateCommand(NewChat);

        public DelegateCommand NavigateToSettingsCommand => new DelegateCommand(async() => await NavigateToSettings());
        #endregion

        #region Constructor
        public ChatPageViewModel(ISemanticScreenReader screenReader, IOpenAiClient openAiClient, IPageDialogService pageDialogService)
        {
            _openAiClient = openAiClient;
            _pageDialogService = pageDialogService;
        }
        #endregion

        #region Methods
        public async Task SendChatInput()
        {
            try
            {
                if (NetworkAccess == "Internet")
                {
                    if (string.IsNullOrEmpty(ChatInputText))
                        throw new ArgumentException("Please enter text to initiate the chat.");
                    //var chat = _openAiClient.GetApiClient().Chat.CreateConversation();
                    //chat.AppendUserInput(ChatInputText);

                    //await foreach (var res in chat.StreamResponseEnumerableFromChatbotAsync())
                    //{
                    //    ConversationText = res;
                    //}

                    IsBusy = true;
                    var model = "";
                    if (ChatInputText.ToLower().Contains("code"))
                        model = OpenAI_API.Models.Model.DavinciCode;
                    ConversationList.Add(new ChatMessage(ChatMessageRole.User, ChatInputText + Environment.NewLine));
                    _messageArray = ConversationList.ToArray();
                    var request = new OpenAI_API.Chat.ChatRequest
                    {
                        Model = OpenAI_API.Models.Model.ChatGPTTurbo,
                        Temperature = 1,
                        MaxTokens = 500,
                        Messages = _messageArray
                    };

                    var response = await _openAiClient.GetConversation(request);

                    AddToConversation(response);

                    DisplayConversation();

                    ChatInputText = "";
                }
                else
                    await _pageDialogService.DisplayAlertAsync("Alert", "No internet connection", "Ok");
            }
            catch (ArgumentException e)
            {
                await _pageDialogService.DisplayAlertAsync("Alert", e.Message, "Ok");
            }
            catch(Exception ex)
            {
                await _pageDialogService.DisplayAlertAsync("Error", ex.Message, "Ok");
            }
        }

        public void AddToConversation(ChatResult response)
        {
            ConversationList.Add(new ChatMessage(response.Choices[0].Message.Role, response.Choices[0].Message.Content));
        }

        public void DisplayConversation()
        {
            var strBuilder = new StringBuilder();
            foreach (var i in ConversationList)
            {
                strBuilder.AppendLine($"{i.Role} : {i.Content}");
            }
            ConversationText = strBuilder.ToString();
            IsBusy = false;
        }

        public void NewChat()
        {
            ChatInputText = "";
            if (ConversationList.Any())
                ConversationList.Clear();
        }

        public async Task NavigateToSettings()
        {
            await _pageDialogService.DisplayAlertAsync("Alert", "Go to Settings", "Ok");
        }
        #endregion

        #region Navigation
        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            IsBusy = false;
            ConversationList = new ObservableCollection<ChatMessage>();
        }
        #endregion
    }
}

