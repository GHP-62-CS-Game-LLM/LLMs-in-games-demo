using System;
using System.Threading.Tasks;
using OllamaSharp;
using OllamaSharp.Models;
using UnityEngine;

public class LllmManager : MonoBehaviour
{
    public string host = "http://localhost:11434";
    public string model = "deepseek-r1:1.5b";
    
    private OllamaApiClient m_ollama;
    
    void Start()
    {
        m_ollama = new OllamaApiClient(new Uri(host));
        m_ollama.SelectedModel = model;
    }

    // public async Task<string> Message(string prompt)
    // {
    //     Chat chat = new Chat(m_ollama);
    //     
    //     chat.SendAsync(message)
    // }
}
