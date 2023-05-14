using AIAssistant.Helpers;
using OpenAI_API.Chat;

namespace AIAssistant.Model
{
    public class ChatMessage : OpenAI_API.Chat.ChatMessage
    {
        public CodeItem CodeItem { get; set; }

        public ChatMessage(ChatMessageRole role, CodeItem codeItem , string content = "")
        {
            Role = role;
            Content = content;
            CodeItem = codeItem;
        }
    }
}

