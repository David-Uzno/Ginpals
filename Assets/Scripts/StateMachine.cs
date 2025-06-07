using System;
using UnityEngine;
using UnityEngine.AI;

public class StateMachine : MonoBehaviour
{

    

    public enum States
    {
        IdleState,
        StalkingState,
        AttackState,
    };
    
    public event Action<Argenimal> OnAttackTriggered;
    
    public States currentState = States.IdleState;
    public float minRange = 0.5f;
    
    [SerializeField]
    private Argenimal _target;

    private float _stunnedTimer;

    private const float MaxStunnedTimer = 3.0f;
    
    private Argenimal[] _enemyTeam;
    private NavMeshAgent _agent;
    private Action<Argenimal> _attackCallback;
    
    private bool _stunned = false;
    private bool _dead = false;

    public void Die()
    {
        _dead = true;
    }
    
    public void Setup(Argenimal[] enemyTeam, NavMeshAgent agent)
    {
        _enemyTeam = enemyTeam;
        _agent = agent;
    }

    public void ProcessStates()
    {
        if (_dead) { return; }
        if (_stunned) { HandleStun(); }
        switch (currentState)
        {
            case States.IdleState:
                currentState = States.StalkingState;
                break;
            case States.StalkingState:
                HandleStalking();
                break;
            case States.AttackState:
                HandleAttack();
                break;
            default:
                break;
        }
    }

    private void HandleStalking() 
    {

        float minDistance = float.MaxValue;
        Argenimal closestEnemy = _enemyTeam[0];
        foreach (var enemy in _enemyTeam) 
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance && !enemy.IsDead())
            {
                minDistance = distance;
                closestEnemy = enemy;
            }
        }
        
        // TODO set check based on attack range
        if (minDistance < minRange && !closestEnemy.IsDead()) // Example distance to switch to attack state
        {
            currentState = States.AttackState;
            _target = closestEnemy;
            return;
        }


        _agent.SetDestination(closestEnemy.transform.position);
        
    }
    
    void HandleAttack()
    {
        OnAttackTriggered?.Invoke(_target);
    }

    private void HandleStun() 
    {
        _stunnedTimer += Time.deltaTime;
        if (_stunnedTimer > MaxStunnedTimer)
        {
            _stunned = false;
            _stunnedTimer = 0.0f;
        }
        else
        {
            return;
        }
    }
}
