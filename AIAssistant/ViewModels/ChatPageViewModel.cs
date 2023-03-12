using System;
namespace AIAssistant.ViewModels
{
    public class ChatPageViewModel : BindableBase, INavigationAware
    {
        #region Public members
        public string ChatInputText { get; set; }

        public string CorrectedText { get; set; }
        #endregion

        #region Commands
        public DelegateCommand SendChatInputCommand => new DelegateCommand(async() => await SendChatInput());
        #endregion

        #region Constructor
        public ChatPageViewModel(ISemanticScreenReader screenReader)
        {
        }
        #endregion

        #region Methods
        public async Task SendChatInput()
        {

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

