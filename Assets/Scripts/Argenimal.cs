using UnityEngine;
using UnityEngine.AI;

public class Argenimal : MonoBehaviour
{
    public enum AttackType
    {
        Melee,
        Ranged
    }
    
    
    public int maxHealth;
    public int health;
    public int attackPower;
    public int attackSpeed = 1;
    public int speed;
    public float range = 0.5f;
    public CombatManager.Teams team;
    public AttackType attackType = AttackType.Melee;

    
    public NavMeshAgent agent;
    public Argenimal[] enemyTeam;
    
    
    private StateMachine _stateMachine;
    private float attackTimer;
    
    private FloatingHealthBar healthBar;

    [SerializeField] private Transform bullet;
    
    public bool IsDead()
    {
        return health == 0;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.UpdateHealthBar(health, maxHealth);
        if (health > 0) return;
        
        health = 0;
        healthBar.UpdateHealthBar(health, maxHealth);
        _stateMachine.Die();
    }
    
    public void StartCombat() 
    {
        _stateMachine.Setup(enemyTeam, range);
        _stateMachine.OnAttackTriggered += Attack;
        _stateMachine.OnTargetAcquired += MoveToTarget;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        
        _stateMachine = gameObject.AddComponent<StateMachine>();
        healthBar = gameObject.GetComponentInChildren<FloatingHealthBar>();
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
        if (!(attackTimer <= 0.0f)) return;
        
        if (attackType == AttackType.Ranged)
        {
            RangeAttack(target);
        }
        else
        {
            target.TakeDamage(attackPower);
        }
        
        
        attackTimer = 1.0f / attackSpeed;

    }

    private void MoveToTarget(Argenimal target)
    {
        agent.SetDestination(target.transform.position);
    }

    private void RangeAttack(Argenimal target)
    {
        Transform bulletTransform = Instantiate(bullet, transform.position, Quaternion.identity);
        Vector3 shootDir = (target.transform.position - transform.position).normalized;
        bulletTransform.GetComponent<Bullet>().Setup(shootDir, attackPower, team);
    }
    
    private void OnDestroy()
    {
        if (!_stateMachine) return;
        
        _stateMachine.OnAttackTriggered -= Attack;
        _stateMachine.OnTargetAcquired -= MoveToTarget;
    }
    
    


}
