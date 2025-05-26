using UnityEngine;
using UnityEngine.AI;

public class Argenimal : MonoBehaviour
{
    public int health;
    public NavMeshAgent agent;


    private StateMachine _stateMachine;

    public Argenimal[] _enemyTeam;

    
    public void StartCombat() 
    {
        _stateMachine.Setup(_enemyTeam, agent);
        return;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        
        _stateMachine = gameObject.AddComponent<StateMachine>();
        StartCombat();
    }

    // Update is called once per frame
    private void Update()
    {
        _stateMachine.ProcessStates();
        
    }
}
