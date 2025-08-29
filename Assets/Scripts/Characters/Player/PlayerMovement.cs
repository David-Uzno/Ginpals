using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private LayerMask solidObjectsLayer;
    
    private bool isMoving;
    private Vector2 input;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!isMoving)
        {
            // Obtener entrada del jugador
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");
            
            // Eliminar movimiento diagonal
            if (input.x != 0) input.y = 0;
            
            if (input != Vector2.zero)
            {
                
                // Calcular nueva posición
                Vector3 targetPos = transform.position + new Vector3(input.x, input.y, 0);
                
                // Verificar si el camino está libre
                if (IsWalkable(targetPos))
                {
                    StartCoroutine(Move(targetPos));
                }
            }
        }
        

    }

    private bool IsWalkable(Vector3 targetPos)
    {
        // Verificar colisiones con objetos sólidos
        if (Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectsLayer) != null)
        {
            return false;
        }
        return true;
    }

    private System.Collections.IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;
        
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPos,
                moveSpeed * Time.deltaTime
            );
            yield return null;
        }
        
        transform.position = targetPos;
        isMoving = false;
    }
}