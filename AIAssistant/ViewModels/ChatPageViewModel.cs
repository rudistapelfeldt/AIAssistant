using System;
using System.Text;
using AIAssistant.Model;
using AIAssistant.OpenAi.Interfaces;
using OpenAI_API.Chat;

namespace AIAssistant.ViewModels
{
    public class ChatPageViewModel : BindableBase, INavigationAware
    {
        #region private members
        IList<ChatMessage> _conversationList = new List<ChatMessage>();
        IList<ChatMessage> _messageArray;
        readonly IOpenAiClient _openAiClient;

        readonly IPageDialogService _pageDialogService;
        #endregion

        #region Public members
        public string ChatInputText { get; set; }

        public string ConversationText { get; set; }
        #endregion

        #region Commands
        public DelegateCommand SendChatInputCommand => new DelegateCommand(async () => await SendChatInput());

        public DelegateCommand NewChatCommand => new DelegateCommand(NewChat);
        #endregion

        #region Constructor
        public ChatPageViewModel(ISemanticScreenReader screenReader, IOpenAiClient openAiClient, IPageDialogService pageDialogService)
        {
            _openAiClient = openAiClient;
            _pageDialogService = pageDialogService;

            _conversationList.Add(new ChatMessage(ChatMessageRole.System, AppConstants.BEHAVIOUR));
        }
        #endregion

        #region Methods
        public async Task SendChatInput()
        {
            try
            {
                if (string.IsNullOrEmpty(ChatInputText))
                    throw new ArgumentException("Please enter text to initiate the chat.");
                _conversationList.Add(new ChatMessage(ChatMessageRole.User, ChatInputText));
                _messageArray = _conversationList.ToArray();
                var request = new OpenAI_API.Chat.ChatRequest
                {
                    Model = OpenAI_API.Models.Model.ChatGPTTurbo,
                    Temperature = 0.1,
                    MaxTokens = 500,
                    Messages = _messageArray
                };

                var response = await _openAiClient.CreateChatCompletionAsync(request);

                AddToConversation(response);

                DisplayConversation();

                ChatInputText = "";

            }
            catch (ArgumentException e)
            {
                await _pageDialogService.DisplayAlertAsync("Alert", e.Message, "Ok");
            }
        }

        public void AddToConversation(ChatResult response)
        {
            _conversationList.Add(new ChatMessage(response.Choices[0].Message.Role, response.Choices[0].Message.Content));
        }

        public void DisplayConversation()
        {
            var strBuilder = new StringBuilder();
            foreach (var i in _conversationList)
            {
                strBuilder.AppendLine($"{i.Role}: {i.Content}");
            }
            ConversationText = strBuilder.ToString();
        }

        public void NewChat()
        {
            ChatInputText = "";
            ConversationText = "";
            if (_conversationList.Any())
                _conversationList.Clear();
        }

        #endregion

        #region Navigation
        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}

