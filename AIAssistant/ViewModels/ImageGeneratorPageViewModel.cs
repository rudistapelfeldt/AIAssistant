using System;
using AIAssistant.OpenAi.Interfaces;

namespace AIAssistant.ViewModels
{
    public class ImageGeneratorPageViewModel : BindableBase
    {
        #region Private members
        readonly IOpenAiClient _openAiClient;

        readonly IPageDialogService _pageDialogService;
        #endregion

        #region Public members
        public string ImageDescriptionText { get; set; }

        public ImageSource GeneratedImage { get; set; }
        #endregion

        #region Commands
        public DelegateCommand GenerateImageCommand => new DelegateCommand(async () => await GenerateImage());
        #endregion

        #region Constructor
        public ImageGeneratorPageViewModel(ISemanticScreenReader screenReader, IOpenAiClient openAiClient, IPageDialogService pageDialogService)
        {
            _openAiClient = openAiClient;
            _pageDialogService = pageDialogService;
        }
        #endregion

        #region Methods
        public async Task GenerateImage()
        {
            try
            {
                if (string.IsNullOrEmpty(ImageDescriptionText))
                    throw new ArgumentException("Please enter a description for the image you wish to generate");
                var response = await _openAiClient.GenerateImage(ImageDescriptionText);
                if (response != null)
                    GeneratedImage = ImageSource.FromUri(new Uri(response.Data[0].Url));
            }
            catch (ArgumentException e)
            {
                await _pageDialogService.DisplayAlertAsync("Alert", e.Message, "Ok");
            }
            catch (Exception ex)
            {
                await _pageDialogService.DisplayAlertAsync("Alert", ex.Message, "Ok");
            }
        }
        #endregion
    }
}

