using UnityEngine;
using UnityEngine.AI;

public class MoverAgente : MonoBehaviour
{
    public NavMeshAgent agent;
    public Camera cam;

    void Start()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 click = cam.ScreenToWorldPoint(Input.mousePosition);
            agent.SetDestination(click);
        }
    }
}
