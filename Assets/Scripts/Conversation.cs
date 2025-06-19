using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OllamaSharp;

public class Conversation
{
    public readonly string Context;
    
    private readonly Chat _chat;

    public Conversation(OllamaApiClient ollama, string context = "")
    {
        Context = context;
        _chat = new Chat(ollama, Context);
    }

    public async Task<string> Message(string prompt)
    {
        StringBuilder sb = new StringBuilder();
        IAsyncEnumerable<string> response = _chat.SendAsync(prompt);
        await foreach (string token in response) sb.Append(token);

        return sb.ToString();
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
