using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Yarn.Unity;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
  [Header("Movement Settings")]
  public float moveSpeed = 5f;

  [Header("Collision Settings")]
  public float collisionOffset = 0.05f;
  public ContactFilter2D movementFilter;

  [Header("Input Settings")]
  public InputAction moveAction;

  [Header("Dialogue System")]
  [SerializeField] private DialogueRunner dialogueRunner;

  [SerializeField] private Animator _animator;
  [SerializeField] private SpriteRenderer _spriteRenderer;

  private Rigidbody2D rb;
  private Vector2 movement;
  private string lastDirection = "Down";
  private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();


  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    rb.gravityScale = 0f;
    rb.freezeRotation = true;
    if (dialogueRunner == null)
        dialogueRunner = FindFirstObjectByType<DialogueRunner>();

      if (dialogueRunner == null)
      {
        Debug.LogWarning("Dialogue Trigger: No DialogueRunner found. Dialogue triggering disabled.");
      }
    moveAction = InputSystem.actions.FindAction("Move");
  }

  // Per frame basis (for inputs)
  void Update()
  {
    movement = moveAction.ReadValue<Vector2>().normalized;

    _animator.SetBool("isWalking", movement != Vector2.zero);
    

    Debug.Log("movement.y = " + movement.y);

    _animator.SetFloat("WalkUp", movement.y);
    _animator.SetFloat("WalkX", Mathf.Abs(movement.x));
    
    if (movement.x > 0)
        _spriteRenderer.flipX = true; // facing right
    else if (movement.x < 0)
        _spriteRenderer.flipX = false;  // facing left


  }

  // Fixed time interval updates (movement ties to time, not framerate)
  void FixedUpdate()
  {
    // Skip movement entirely if dialogue is active
    if (dialogueRunner.IsDialogueRunning) return;

    // Only run if the player is actually trying to move
    if (movement != Vector2.zero)
    {
      // Try to move in the direction of the input
      bool success = TryMove(movement);

      // If we hit a wall while moving diagonally, try moving just horizontally or just vertically
      // so the player doesn't get completely stuck on a corner.
      if (!success && movement.x != 0 && movement.y != 0)
      {
        success = TryMove(new Vector2(movement.x, 0)); // Try X axis only

        if (!success)
        {
          TryMove(new Vector2(0, movement.y)); // Try Y axis only
        }
      }
    }
  }

  // This function returns TRUE if the player successfully moved, and FALSE if they hit a wall
  private bool TryMove(Vector2 direction)
  {
    // Check for collisions in the direction we want to move
    int count = rb.Cast(
        direction,      // Vector2 of values between -1 and 1
        movementFilter, // The settings that determine what layers we can collide with
        castCollisions, // Recording the collisions
        moveSpeed * Time.fixedDeltaTime + collisionOffset // The amount of distance to cast out
    );

    // If count is 0, no walls
    if (count == 0)
    {
      rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
      return true;
    }
    else
    {
      // We hit a wall and the character stands still
      return false;
    }
  }

  // InputSystem stuff, compiler will get angry if we don't have this
  // void OnEnable()
  // {
  //   moveAction.Enable();
  // }

  // void OnDisable()
  // {
  //   moveAction.Disable();
  // }

}
