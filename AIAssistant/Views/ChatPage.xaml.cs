using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
using OpenAI_API.Chat;

namespace AIAssistant.Views;

public partial class ChatPage : ContentPage
{
    ObservableCollection<ChatMessage> _list;

    public ChatPage()
    {
        InitializeComponent();
    }
}
