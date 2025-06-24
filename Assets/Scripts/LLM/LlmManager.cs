using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OllamaSharp;
using UnityEngine;

/*
- Make text not restart every prompt
- Fix horizontal Scroll Bar
- 
*/

public class LlmManager : MonoBehaviour
{
    public string host = "http://10.40.96.252:8080";

    public SceneContextManager scm;

    private readonly OllamaApiClient _ollama;
    private const string Model = "phi4-mini";

    public LlmManager()
    {
        _ollama = new OllamaApiClient(new Uri(host));
        _ollama.SelectedModel = Model;
    }

    public async Task<string> Message(string prompt)
    {
        Chat chat = new Chat(_ollama);

        StringBuilder sb = new StringBuilder();
        IAsyncEnumerable<string> response = chat.SendAsync(prompt);
        await foreach (string token in response) sb.Append(token);

        return sb.ToString();
    }

    public Conversation MakeConversation(string context) => new Conversation(_ollama, context, () => scm.GetDynamicContext());
}
