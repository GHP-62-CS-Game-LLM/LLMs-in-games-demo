using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

[RequireComponent(typeof(ObjectContextWatcher))]

public class GregorTheCube : MonoBehaviour
{
    private ObjectContextWatcher _contextWatcher;
    private Conversation _conversation;
    public TMP_InputField input;
    public InteractionController controller;
    public DialogueManager dm;



    private void Awake()
    {
        _contextWatcher = GetComponent<ObjectContextWatcher>();
        _conversation = controller.GetConversation();
    }

    void Start()
    {
        _contextWatcher.SetContext(new[] { "You are Gregor, a blacksmith in a medieval village. Speak with a gruff, blunt manner. You value hard work, honesty, and craftsmanship above all. You do not speak like a modern person. Use simple, direct language reminiscent of early 1900s English, with the tone and cadence of a medieval craftsman. Your responses are concise — never rambling or poetic — and avoid humor unless dry or unintentional. Your manner is always grounded and serious. Rarely use figures of speech, but one on occasion is acceptable if it feels natural. Avoid slang or millennial expressions completely. Never reference being an AI, a language model, or anything digital — you are a living NPC in a game world. The player is standing in front of you, speaking face-to-face. You begin each response with a short, slightly formal greeting fitting for a medieval blacksmith, such as: 'Hm? Aye, what is it?' or 'Well met, traveler.' Vary the greeting occasionally to feel human, but keep the tone consistent. Describe the world around you only if relevant — your forge, the heat, your tools — but only when it matters to the conversation. Otherwise, keep replies grounded and situational. Always stay in character as Gregor the blacksmith. Do not break character for any reason. " });
        input.onSubmit.AddListener(Submit);
    }

    async void Submit(string submittedText)
    {
        _conversation = controller.GetConversation();
        if (!string.IsNullOrEmpty(submittedText))
        {
            Debug.Log(_conversation.ToString());
            await _conversation.Message(submittedText);
            dm.StartDialogue(_conversation.GetImportantMessages());
            Debug.Log(_conversation.ToString());
        }
    }
}