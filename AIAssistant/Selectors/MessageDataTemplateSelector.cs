using System;
namespace AIAssistant.Selectors
{
    public class MessageDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate AssistantDataTemplate { get; set; }

        public DataTemplate UserDataTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return ((OpenAI_API.Chat.ChatMessage)item).Role == OpenAI_API.Chat.ChatMessageRole.Assistant ? AssistantDataTemplate : UserDataTemplate;
        }
    }
}

