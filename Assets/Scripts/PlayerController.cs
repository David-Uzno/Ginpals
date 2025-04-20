using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private bool faceMovementDirection = true;

    // Components
    private Rigidbody2D _rb;
    private Vector2 _movementInput;
    private Vector2 _lastMovementDirection;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        
        // Configure rigidbody for top-down movement
        if (_rb != null)
        {
            _rb.gravityScale = 0f;
            _rb.freezeRotation = true;
        }
        
        _lastMovementDirection = Vector2.down; // Default facing direction
    }

    private void Update()
    {
        // Handle input
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        _movementInput = new Vector2(horizontalInput, verticalInput).normalized;
        
        // Update facing direction if moving
        if (_movementInput.magnitude > 0.1f)
        {
            _lastMovementDirection = _movementInput;
        }
    }

    private void FixedUpdate()
    {
        // Apply movement in FixedUpdate for physics consistency
        Move();
    }
    
    private void Move()
    {
        _rb.linearVelocity = _movementInput * moveSpeed;
    }
}