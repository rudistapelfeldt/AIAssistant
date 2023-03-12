using System;
namespace AIAssistant.ViewModels
{
    public class ImageGeneratorPageViewModel : BindableBase
    {
        #region Public members
        public string ImageDescriptionText { get; set; }

        public ImageSource GeneratedImage { get; set; }
        #endregion

        #region Commands
        public DelegateCommand GenerateImageCommand => new DelegateCommand(async () => await GenerateImage());
        #endregion

        #region Constructor
        public ImageGeneratorPageViewModel(ISemanticScreenReader screenReader)
        {
        }
        #endregion

        #region Methods
        public async Task GenerateImage()
        {

        }
        #endregion
    }
}

