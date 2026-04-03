using UnityEngine;
using UnityEngine.InputSystem;
using Yarn.Unity;

public class TriggerDialogue : MonoBehaviour
{
    public DialogueRunner dialogueRunner;
    public string startNode = "NodeName";
    // set this to the name of the node you want to start when the player interacts

    public InputAction interactAction; 
    private bool playerInRange = false;

    void Start()
    {
        interactAction = InputSystem.actions.FindAction("Interact");
        // Find DialogueRunner if not assigned in Inspector
        if (dialogueRunner == null)
            dialogueRunner = FindFirstObjectByType<DialogueRunner>();

        if (dialogueRunner == null)
        {
            Debug.LogWarning("Dialogue Trigger: No DialogueRunner found. Dialogue triggering disabled.");
        }
    }

    void Update()
    {
        // Adjust input key to match whatever your partners used
        if (playerInRange && interactAction.WasPressedThisFrame())
        {
            if (!dialogueRunner.IsDialogueRunning)
                dialogueRunner.StartDialogue(startNode);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            other.transform.Find("InteractIcon").gameObject.SetActive(true); // Show interact icon when player is in range
            Debug.Log("Player entered dialogue trigger area.");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            other.transform.Find("InteractIcon").gameObject.SetActive(false);
            playerInRange = false;
    }
}