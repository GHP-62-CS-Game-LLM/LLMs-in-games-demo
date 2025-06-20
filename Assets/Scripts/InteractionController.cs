using System.Diagnostics;
using System.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using Debug = UnityEngine.Debug;

public class InteractionController : MonoBehaviour
{
    public LlmManager manager;
    public DialogueManager dm;

    public Camera playerCamera;
    
    public float maxInteractDist = 2.5f;
    private InputAction _interactAction;

    private Stopwatch _stopwatch = new Stopwatch();

    private Task<string>? _conversationTask = null;
    [CanBeNull]
    private Conversation _currentConversation;

    private void Start()
    {
        _interactAction = InputSystem.actions.FindAction("Interact");
    }

    private void Update()
    {
        bool canInteract = false;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.TransformDirection(Vector3.forward), out RaycastHit hit,
                maxInteractDist))
        {
            canInteract = hit.collider.CompareTag("Interactable");
        }
    
        if (_interactAction.WasPressedThisFrame() && canInteract)
        {
            Debug.Log("Interacting!");
            string[] context = hit.collider.gameObject.GetComponent<ObjectContextWatcher>().GetContext();
            _currentConversation = manager.MakeConversation(context);
            _stopwatch.Restart();
            _conversationTask = _currentConversation.Message("Hello! What's your name?");
        }

        if (_conversationTask is { IsCompleted: true } && _currentConversation != null)
        {
            _stopwatch.Stop();
            Debug.Log($"Elapsed Time: {_stopwatch.Elapsed}");
            Debug.Log($"Conversation: {_currentConversation}");
            dm.StartDialogue(_currentConversation.ToString());
            _conversationTask = null;
        }
    }
}
