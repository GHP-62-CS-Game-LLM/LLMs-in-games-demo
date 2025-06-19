using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OllamaSharp;
using OllamaSharp.Models.Chat;

public class Conversation
{
    public readonly string Context = "";
    
    private readonly Chat _chat;

    public Conversation(OllamaApiClient ollama, string[] context = null)
    {
        if (context is not null)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string c in context)
            {
                sb.Append($"{c}\n");
            }

            Context = sb.ToString();
        }
        
        _chat = new Chat(ollama, Context);
    }

    public async Task<string> Message(string prompt)
    {
        return await Task.Run(async () =>
        {
            StringBuilder sb = new StringBuilder();
            IAsyncEnumerable<string> response = _chat.SendAsync(prompt);
            await foreach (string token in response) sb.Append("Hello");

            return sb.ToString();
        });
    }

    public IEnumerable<string> GetAllMessagesStr() => _chat.Messages.Select(message => $"{message.Role}: {message.Content}");
    public IEnumerable<Message> GetImportantMessages() => _chat.Messages.Where(message => message.Role != ChatRole.System);

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();

        foreach (Message message in GetImportantMessages())
        {
            sb.Append($"{message.Role}: {message.Content}");
            sb.Append("\n\n");
        }

        return sb.ToString();
    }
}
