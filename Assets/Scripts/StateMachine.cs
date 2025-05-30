using System;
using UnityEngine;
using UnityEngine.AI;
using Debug = System.Diagnostics.Debug;

public class StateMachine : MonoBehaviour
{
    private Argenimal[] _enemyTeam;
    private NavMeshAgent _agent;
    private Action<Argenimal> _attackCallback;
    
    [SerializeField]
    private Argenimal _target;
    public enum States
    {
        IdleState,
        StalkingState,
        AttackState,
    };

    private bool _stunned = false;
    private bool _dead = false;
  

    public States _currentState = States.IdleState;
    public float minRange = 0.5f;

    private float _stunnedTimer;

    private const float MaxStunnedTimer = 3.0f;

    public void Die()
    {
        _dead = true;
    }
    
    public void Setup(Argenimal[] enemyTeam, NavMeshAgent agent, Action<Argenimal> AttackCallback)
    {
        _enemyTeam = enemyTeam;
        _agent = agent;
        _attackCallback = AttackCallback;
    }

    public void ProcessStates()
    {
        if (_dead) { return; }
        if (_stunned) { HandleStun(); }
        switch (_currentState)
        {
            case States.IdleState:
                _currentState = States.StalkingState;
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
            _currentState = States.AttackState;
            _target = closestEnemy;
            return;
        }


        _agent.SetDestination(closestEnemy.transform.position);
        
    }
    
    void HandleAttack()
    {
        _attackCallback(_target);
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
