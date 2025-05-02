using UnityEngine;
using UnityEngine.AI;

public class MoverAgente : MonoBehaviour
{
    public NavMeshAgent agent;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 click = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            click.y = 0;
            agent.SetDestination(click);
        }

        // Mantener en Z=0 si es 2D
        Vector3 pos = agent.transform.position;
        pos.y = 0;
        agent.transform.position = pos;
    }
}
