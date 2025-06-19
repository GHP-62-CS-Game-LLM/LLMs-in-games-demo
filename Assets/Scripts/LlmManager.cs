using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OllamaSharp;
using UnityEngine;

public class LlmManager : MonoBehaviour
{
    public string host = "http://localhost:11434";

    public SceneContextManager scm;
    
    private readonly OllamaApiClient _ollama;
    private const string Model = "phi3:3.8b";

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

    public Conversation MakeConversation(string[] context) => new Conversation(_ollama, context);
    public Conversation MakeConversation() => new Conversation(_ollama);
}
