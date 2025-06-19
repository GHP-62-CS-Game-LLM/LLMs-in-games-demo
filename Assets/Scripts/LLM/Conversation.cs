using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OllamaSharp;

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

    public IEnumerable<string> GetAllMessages() => _chat.Messages.Select(message => $"{message.Role}: {message.Content}");

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();

        foreach (string message in GetAllMessages())
        {
            sb.Append(message);
            sb.Append("\n\n");
        }

        return sb.ToString();
    }
}
