using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class TalkingCube : MonoBehaviour
{
    public LlmManager manager;
    
    private Conversation _conversation;
    
    async void Start()
    {
        Debug.Log("Starting Convo");
        _conversation = manager.MakeConversation();

        Stopwatch stopwatch = new Stopwatch();
    
        stopwatch.Start();
        await _conversation.Message("Hello, I am a talking cube!");
        await _conversation.Message("I am a 1x1x1m cube floating in space");
        await _conversation.Message("What are you?");
        stopwatch.Stop();
        
        Debug.Log($"Conversation: {_conversation}");
        Debug.Log($"Elapsed Time: {stopwatch.Elapsed}");
    }
}
