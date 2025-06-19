using System.Diagnostics;
using TMPro;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class TalkingCube : MonoBehaviour
{
    public LlmManager manager;
    public TextMeshProUGUI text;
    private Conversation _conversation;
    private ObjectContextWatcher _ocw;

    async void Start()
    {
        Debug.Log("Starting Convo");
        _conversation = manager.MakeConversation("You are Gregor, the blacksmith. Respond gruffly but honestly. You value hard work.");

        Stopwatch stopwatch = new Stopwatch();

        stopwatch.Start();
        await _conversation.Message("USER: Hello! What's your name?");
        stopwatch.Stop();

        Debug.Log($"Conversation:\n{_conversation}");
        text.SetText($"Elapsed Time: {stopwatch.Elapsed}\nCube:\n{_conversation}");
        Debug.Log($"Elapsed Time: {stopwatch.Elapsed}");
    }

    /* testing for context sys

    void Awake()
    {
        _ocw = GetComponent<ObjectContextWatcher>();
    }

    void Update()
    {
        if (_ocw)
            _ocw.SetContext(new string[] { "testing" });
    }
    */
}
