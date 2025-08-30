using UnityEngine;
using UnityEngine.SceneManagement;

public class GrassObsolete : MonoBehaviour
{
    [SerializeField, Range(0, 100)]
    private float probabilityToAdvance = 100f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Random.Range(0f, 100f) < probabilityToAdvance)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}
