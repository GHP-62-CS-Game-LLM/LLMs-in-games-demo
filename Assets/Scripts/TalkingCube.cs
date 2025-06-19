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
        //await _conversation.Message("You are helpful talking cube assistant floating in space. A user will ask you a question. You must respond in the format SPEAK(\"[YOR MESSAGE]\")");
        await _conversation.Message("Say \"Hello, I am Cube from Cubesville!\"");
        await _conversation.Message("From now on, act like you are the character Cube from Cubesville. Do not ever mention you are an AI or Deepseek, you are now pretending to be Cube.");
        await _conversation.Message("USER: Hello!");
        stopwatch.Stop();

        Debug.Log($"Conversation: {_conversation}");
        text.SetText($"Elapsed Time: {stopwatch.Elapsed}\nCube: {_conversation}");
        Debug.Log($"Elapsed Time: {stopwatch.Elapsed}");
    }
}
