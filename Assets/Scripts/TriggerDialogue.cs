using UnityEngine;
using UnityEngine.InputSystem;
using Yarn.Unity;

public class TriggerDialogue : MonoBehaviour
{
    public DialogueRunner dialogueRunner;
    public string startNode = "NodeName";

    public InputAction interactAction; 
    private bool playerInRange = false;

    private ParticleSystem sparkleParticles;

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

        CreateSparkleEffect();
    }

    void CreateSparkleEffect()
    {
        GameObject sparkleObj = new GameObject("Sparkles");
        sparkleObj.transform.SetParent(this.transform);
        sparkleObj.transform.localPosition = new Vector3(0, 0.25f, 0);
        
        sparkleParticles = sparkleObj.AddComponent<ParticleSystem>();
        sparkleParticles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        
        var main = sparkleParticles.main;
        main.duration = 1f;
        main.loop = true;
        main.startLifetime = 1.2f;
        main.startSpeed = 0.2f;
        main.startSize = 0.15f;
        main.startColor = new Color(1f, 1f, 0.8f, 0.8f);
        main.simulationSpace = ParticleSystemSimulationSpace.World;
        
        var emission = sparkleParticles.emission;
        emission.rateOverTime = 2f;
        
        var shape = sparkleParticles.shape;
        shape.shapeType = ParticleSystemShapeType.Circle;
        shape.radius = 0.5f;
        
        var colOverLife = sparkleParticles.colorOverLifetime;
        colOverLife.enabled = true;
        Gradient grad = new Gradient();
        grad.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color.white, 0.0f), new GradientColorKey(Color.white, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(0.0f, 0.0f), new GradientAlphaKey(1.0f, 0.5f), new GradientAlphaKey(0.0f, 1.0f) }
        );
        colOverLife.color = grad;

        var rotOverLife = sparkleParticles.rotationOverLifetime;
        rotOverLife.enabled = true;
        rotOverLife.z = new ParticleSystem.MinMaxCurve(0f, 180f);

        var renderer = sparkleParticles.GetComponent<ParticleSystemRenderer>();
        if (Shader.Find("Sprites/Default") != null) {
            renderer.material = new Material(Shader.Find("Sprites/Default"));
        }
        // Using "Player" sorting layer since it exists in TagManager and is rendered on top
        renderer.sortingLayerName = "Player"; 
        renderer.sortingOrder = 100;
        
        sparkleParticles.Play();
    }

    void Update()
    {
        // Adjust input key to match whatever your partners used
        if (playerInRange && interactAction.WasPressedThisFrame())
        {
            if (!dialogueRunner.IsDialogueRunning)
                dialogueRunner.StartDialogue(startNode);
        }

        // Hide sparkles while dialogue is running
        if (sparkleParticles != null && dialogueRunner != null)
        {
            if (dialogueRunner.IsDialogueRunning && sparkleParticles.isPlaying)
            {
                sparkleParticles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            }
            else if (!dialogueRunner.IsDialogueRunning && !sparkleParticles.isPlaying)
            {
                sparkleParticles.Play();
            }
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
        {
            other.transform.Find("InteractIcon").gameObject.SetActive(false);
            playerInRange = false;
        }
    }
}