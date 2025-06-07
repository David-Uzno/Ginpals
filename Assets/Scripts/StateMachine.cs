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
        VictoryState,
    };
    
    public event Action<Argenimal> OnAttackTriggered;
    public event Action<Argenimal> OnTargetAcquired;
    
    public States currentState = States.IdleState;
    public float minRange = 0.5f;
    
    [SerializeField]
    private Argenimal _target;

    private float _stunnedTimer;

    private const float MaxStunnedTimer = 3.0f;
    
    private Argenimal[] _enemyTeam;
    
    private bool _stunned = false;
    private bool _dead = false;

    public void Die()
    {
        _dead = true;
    }
    
    public void Setup(Argenimal[] enemyTeam)
    {
        _enemyTeam = enemyTeam;
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
            return;
        }


        OnTargetAcquired?.Invoke(closestEnemy);
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
