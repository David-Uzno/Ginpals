using System;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public enum States
    {
        IdleState,
        StalkingState,
        AttackState,
        VictoryState,
    };
    
    public event Action<Argenimal> OnAttackTriggered;
    public event Action<Vector3> OnTargetAcquired;
    
    public States currentState = States.IdleState;
    public float minRange = 0.5f;
    
    [SerializeField]
    private Argenimal _target;

    private float _stunnedTimer;

    private const float MaxStunnedTimer = 3.0f;
    
    private Argenimal[] _enemyTeam;
    
    private bool _stunned;
    private bool _dead;
    private bool _inCombat;

    public void Die()
    {
        _dead = true;
    }
    
    public void Setup(Argenimal[] enemyTeam, float attackRange)
    {
        _enemyTeam = enemyTeam;
        minRange = attackRange;
        _inCombat = true;
    }

    public void ProcessStates()
    {
        if (_dead) { return; }
        if (_stunned) { HandleStun(); }
        if(!_inCombat){return;}
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
            case States.VictoryState:
            default:
                break;
        }
    }

    private void HandleStalking() 
    {
        Argenimal closestEnemy = GetClosestEnemy();

        if (!closestEnemy)
        {
            currentState = States.VictoryState;
            return;
        }
        
        if (Vector3.Distance(transform.position, closestEnemy.transform.position) < minRange)
        {
            currentState = States.AttackState;
            _target = closestEnemy;
            OnTargetAcquired?.Invoke(transform.position);
            return;
        }

        OnTargetAcquired?.Invoke(closestEnemy.transform.position);
    }
    
    private Argenimal GetClosestEnemy()
    {
        float minDistance = float.MaxValue;
        Argenimal closestEnemy = null;
        foreach (var enemy in _enemyTeam)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance && !enemy.IsDead())
            {
                minDistance = distance;
                closestEnemy = enemy;
            }
        }
        return closestEnemy;
    }

    private void HandleAttack()
    {
        if (_target == null || _target.IsDead())
        {
            currentState = States.StalkingState;
            return;
        }
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
    }
}
