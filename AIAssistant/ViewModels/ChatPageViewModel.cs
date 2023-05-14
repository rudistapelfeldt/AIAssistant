using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using AIAssistant.Helpers;
using AIAssistant.Model;
using AIAssistant.OpenAi.Interfaces;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using OpenAI_API.Chat;
using ChatMessage = OpenAI_API.Chat.ChatMessage;

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

        public DelegateCommand NavigateToSettingsCommand => new DelegateCommand(async () => await NavigateToSettings());
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

                    IsBusy = true;
                    var model = "";
                    if (ChatInputText.ToLower().Contains("code"))
                        model = OpenAI_API.Models.Model.DavinciCode;
                    ConversationList.Add(new Model.ChatMessage(ChatMessageRole.User, null , ChatInputText + Environment.NewLine));
                    _messageArray = ConversationList.ToArray();
                    var request = new OpenAI_API.Chat.ChatRequest
                    {
                        Model = OpenAI_API.Models.Model.ChatGPTTurbo,
                        Temperature = 1,
                        MaxTokens = 500,
                        Messages = (IList<OpenAI_API.Chat.ChatMessage>)_messageArray
                    };

                    var response = await _openAiClient.GetConversation(request);

                    AddToConversation(response);

                    ChatInputText = "";
                }
                else
                    await _pageDialogService.DisplayAlertAsync("Alert", "No internet connection", "Ok");
            }
            catch (ArgumentException e)
            {
                await _pageDialogService.DisplayAlertAsync("Alert", e.Message, "Ok");
            }
            catch (Exception ex)
            {
                await _pageDialogService.DisplayAlertAsync("Error", ex.Message, "Ok");
            }
        }

        public void AddToConversation(ChatResult response)
        {
            if (response.Choices[0].Message.Content.Contains("```"))
                ConversationList.Add(new Model.ChatMessage(response.Choices[0].Message.Role, null, response.Choices[0].Message.Content.Replace("```", "")));
            else
                ConversationList.Add(new Model.ChatMessage(response.Choices[0].Message.Role, null , response.Choices[0].Message.Content));
            IsBusy = false;
        }

        public Model.ChatMessage FormatCodeBlock(ChatResult response)
        {
            var codeBlock = new FormattedString();

            codeBlock.Spans.Add(new Span { Text = "public " });
            codeBlock.Spans.Add(new Span { Text = "class ", TextColor = Colors.Blue });
            codeBlock.Spans.Add(new Span { Text = "MyClass", TextColor = Colors.Red });
            codeBlock.Spans.Add(new Span { Text = "\n{\n    " });
            codeBlock.Spans.Add(new Span { Text = "// Comment", TextColor = Colors.Green });
            codeBlock.Spans.Add(new Span { Text = "\n    " });
            codeBlock.Spans.Add(new Span { Text = "string ", TextColor = Colors.Purple });
            codeBlock.Spans.Add(new Span { Text = "myString", TextColor = Colors.Red });
            codeBlock.Spans.Add(new Span { Text = " = " });
            codeBlock.Spans.Add(new Span { Text = "\"Hello, world!\"", TextColor = Colors.Brown });
            codeBlock.Spans.Add(new Span { Text = ";\n}", TextColor = Colors.Transparent });

            return new Model.ChatMessage(response.Choices[0].Message.Role, new CodeItem { Code = codeBlock });
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

