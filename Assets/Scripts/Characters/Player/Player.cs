using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _walkSpeed = 4f;
    [SerializeField] private float _runSpeed = 6f;
    [SerializeField] private byte _stepDistance = 1;
    [SerializeField] private LayerMask _solidObjectsLayer;
    private bool _isMoving;

    [Header("Dependencies")]
    [SerializeField] private PlayerInput _playerInput;

    private void Awake()
    {
        if (_playerInput == null)
        {
            _playerInput = GetComponent<PlayerInput>();
        }
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        Vector2 movementInput = _playerInput.actions["Move"].ReadValue<Vector2>();

        if (!_isMoving)
        {
            if (movementInput != Vector2.zero)
            {
                // Priorizar movimiento horizontal o vertical, pero no ambos
                if (Mathf.Abs(movementInput.x) > Mathf.Abs(movementInput.y))
                {
                    movementInput.y = 0;
                }
                else
                {
                    movementInput.x = 0;
                }

                // Calcular la posición objetivo
                Vector3 targetPosition = transform.position + new Vector3(movementInput.x, movementInput.y, 0) * _stepDistance;

                if (IsWalkable(targetPosition))
                {
                    StartCoroutine(Move(targetPosition));
                }
            }
        }
    }

    private bool IsWalkable(Vector3 targetPosition)
    {
        return !Physics2D.OverlapCircle(targetPosition, 0.2f, _solidObjectsLayer);
    }

    private System.Collections.IEnumerator Move(Vector3 targetPosition)
    {
        _isMoving = true;

        while ((targetPosition - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, _walkSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPosition;
        _isMoving = false;
    }
}