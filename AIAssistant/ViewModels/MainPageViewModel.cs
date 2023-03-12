using System.Diagnostics.Tracing;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using AIAssistant.Model;
using AIAssistant.OpenAi.Interfaces;

namespace AIAssistant.ViewModels;

public class MainPageViewModel : BindableBase
{
    #region Private members
    readonly IOpenAiClient _client;

    readonly IPageDialogService _pageDialogService;

    private ISemanticScreenReader _screenReader { get; }
    #endregion

    #region Public members
    public string ResponseText { get; set; }

    public string Title => "AI Assistant";

    public string RequestText { get; set; }
    #endregion

    #region Commands
    public DelegateCommand SubmitRequestCommand => new DelegateCommand(async() => await SubmitRequest());
    #endregion

    #region Constructor
    public MainPageViewModel(ISemanticScreenReader screenReader, IPageDialogService pageDialogService, IOpenAiClient client)
    {
        _pageDialogService = pageDialogService;
        _client = client;
        _screenReader = screenReader;
    }
    #endregion

    #region Methods
    public async Task SubmitRequest()
    {
        try
        {
            VerifyInput();
            var response = await _client.GetCompletionText(RequestText, ResponseText);
            ResponseText = response.Completions.FirstOrDefault().Text;
        }
        catch (ArgumentException e)
        {
            await _pageDialogService.DisplayAlertAsync("Attention", e.Message, "Ok");
        }
    }

    public void VerifyInput()
    {
        if (string.IsNullOrEmpty(RequestText))
                throw new ArgumentException("Please enter input");
    }
    #endregion
}
