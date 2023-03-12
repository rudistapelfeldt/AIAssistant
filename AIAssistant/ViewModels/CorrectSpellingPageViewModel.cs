using System;
namespace AIAssistant.ViewModels
{
    public class CorrectSpellingPageViewModel : BindableBase
    {
        #region Public members
        public string InputText { get; set; }

        public string ResponseText { get; set; }
        #endregion

        #region Command
        public DelegateCommand CorrectCommand => new DelegateCommand(async() => await Correct());
        #endregion

        #region Constructor
        public CorrectSpellingPageViewModel(ISemanticScreenReader screenReader)
        {
        }
        #endregion

        #region Methods
        public async Task Correct()
        {

        }
        #endregion
    }
}

