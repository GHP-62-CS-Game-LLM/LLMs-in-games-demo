using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OllamaSharp;

public class Conversation
{
    private readonly Chat _chat;

    public Conversation(OllamaApiClient ollama)
    {
        _chat = new Chat(ollama, "You are Gregor, the blacksmith. Respond gruffly but honestly. You value hard work.");
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
