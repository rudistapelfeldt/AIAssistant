using System;
namespace AIAssistant.ViewModels
{
    public class FlyOutMainPageViewModel : BindableBase, INavigationAware
    {
        #region Private members
        readonly INavigationService _navigationService;
        #endregion

        #region Commands
        public DelegateCommand NavigateToAIChatCommand => new DelegateCommand(async() => await NavigateToAIChat());
        public DelegateCommand NavigateToImageGeneratorCommand => new DelegateCommand(async () => await NavigateToImageGenerator());
        public DelegateCommand NavigateToCorrectionCommand => new DelegateCommand(async () => await NavigateToCorrection());
        public DelegateCommand NavigateToCompletionCommand => new DelegateCommand(async () => await NavigateToCompletion());
        #endregion

        #region Constructor
        public FlyOutMainPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        #endregion

        #region Methods
        public async Task NavigateToAIChat()
        {
            await _navigationService.NavigateAsync("FlyOutMainPage/NavigationPage/ChatPage");
        }

        public async Task NavigateToImageGenerator()
        {
            await _navigationService.NavigateAsync("FlyOutMainPage/NavigationPage/ChatPage");
        }

        public async Task NavigateToCorrection()
        {
            await _navigationService.NavigateAsync("FlyOutMainPage/NavigationPage/ChatPage");
        }

        public async Task NavigateToCompletion()
        {
            await _navigationService.NavigateAsync("FlyOutMainPage/NavigationPage/MainPage");
        }
        #endregion

        #region Navigation
        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            
        }
        #endregion
    }
}

