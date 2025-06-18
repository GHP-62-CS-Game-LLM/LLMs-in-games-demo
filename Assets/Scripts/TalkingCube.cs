using TMPro.EditorUtilities;
using UnityEngine;

public class TalkingCube : MonoBehaviour
{
    public LlmManager manager;
    
    private Conversation _conversation;
    
    async void Start()
    {
        Debug.Log("Starting Convo");
        _conversation = manager.MakeConversation();

        await _conversation.Message("Hello, I am a talking cube!");
        await _conversation.Message("I am a 1x1x1m cube floating in space");
        await _conversation.Message("What are you?");
        
        Debug.Log($"Conversation: {_conversation}");
    }
}
