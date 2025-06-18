using System.Diagnostics;
using TMPro;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class TalkingCube : MonoBehaviour
{
    public LlmManager manager;
    public TextMeshProUGUI text;
    
    private Conversation _conversation;
    
    async void Start()
    {
        Debug.Log("Starting Convo");
        _conversation = manager.MakeConversation();

        Stopwatch stopwatch = new Stopwatch();
    
        stopwatch.Start();
        await _conversation.Message("You are helpful talking cube assistant floating in space. A user will ask you a question. You must respond in the format SPEAK(\"[YOR MESSAGE]\")");
        await _conversation.Message("USER: Hello!");
        stopwatch.Stop();
        
        Debug.Log($"Conversation: {_conversation}");
        text.SetText($"Elapsed Time: {stopwatch.Elapsed}\nConversation: {_conversation}");
        Debug.Log($"Elapsed Time: {stopwatch.Elapsed}");
    }
}
