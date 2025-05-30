using Unity.Mathematics.Geometry;
using UnityEngine;
using UnityEngine.AI;

public class Argenimal : MonoBehaviour
{
   
    public int health;
    public int attackPower;
    public int attackSpeed = 1;
    public int speed;

    
    public NavMeshAgent agent;
    public Argenimal[] _enemyTeam;
    
    
    private StateMachine _stateMachine;
    private Argenimal targetEnemy;
    private float attackTimer;


    public bool IsDead()
    {
        return health == 0;
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {  
            health = 0;
            _stateMachine.Die();
        }
    }
    
    public void StartCombat() 
    {
        _stateMachine.Setup(_enemyTeam, agent, Attack);
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
        attackTimer -= Time.deltaTime;
    }

    private void Attack(Argenimal target)
    {
        if(attackTimer <= 0.0f)
        {
            target.TakeDamage(attackPower);
            attackTimer = 1.0f / (float) attackSpeed;
        }

    }
    
    


}
