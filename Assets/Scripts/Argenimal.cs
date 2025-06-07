using UnityEngine;
using UnityEngine.AI;

public class Argenimal : MonoBehaviour
{
   
    public int health;
    public int attackPower;
    public int attackSpeed = 1;
    public int speed;
    public int range;

    
    public NavMeshAgent agent;
    public Argenimal[] enemyTeam;
    
    
    private StateMachine _stateMachine;
    private float attackTimer;


    public bool IsDead()
    {
        return health == 0;
    }

    private void TakeDamage(int damage)
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
        _stateMachine.Setup(enemyTeam, agent);
        _stateMachine.OnAttackTriggered += Attack;
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
            attackTimer = 1.0f / attackSpeed;
        }

    }
    
    private void OnDestroy()
    {
        if (_stateMachine)
        {
            _stateMachine.OnAttackTriggered -= Attack;
        }
    }
    
    


}
