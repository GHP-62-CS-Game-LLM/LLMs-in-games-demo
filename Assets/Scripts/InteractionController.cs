using System.Diagnostics;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using Debug = UnityEngine.Debug;

public class InteractionController : MonoBehaviour
{
    public LlmManager manager;
    public DialogueManager dm;
    public FirstPersonController fpc;

    public Camera playerCamera;
    
    public float maxInteractDist = 2.5f;
    private InputAction _interactAction;

    private Stopwatch _stopwatch = new Stopwatch();

    private Task<string> _conversationTask;
    private Conversation _currentConversation;

    private bool _isInteracting = false;

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
            _isInteracting = true;
            fpc.canMove = false;
            string context = hit.collider.gameObject.GetComponent<ObjectContextWatcher>().GetContext();
            _currentConversation = manager.MakeConversation(context);
            _stopwatch.Restart();
            //_conversationTask = _currentConversation.Message("Hello! How are you doing today?");
            _conversationTask = _currentConversation.Message("Hello! Can you tell me around what time of day it is?");
        }

        if (_conversationTask is { IsCompleted: true } && _currentConversation != null)
        {
            _isInteracting = false;
            fpc.canMove = true;
            
            _stopwatch.Stop();
            Debug.Log($"Elapsed Time: {_stopwatch.Elapsed}");
            Debug.Log($"Conversation: {_currentConversation}");
            dm.StartDialogue(_currentConversation.ToString());
            _conversationTask = null;
        }
    }
}
