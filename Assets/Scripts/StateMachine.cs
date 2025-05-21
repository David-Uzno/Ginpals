using UnityEngine;

public class StateMachine : MonoBehaviour
{
    Argenimal[] _enemy_team;
    enum States
    {
        IDLE_STATE,
        STALKING_STATE,
        ATTACK_STATE,
    };
    bool stunned = false;
    bool dead = false;

    States current_state = States.IDLE_STATE;

    float stunned_timer;

    const float MAX_STUNNED_TIMER = 3.0f;

    // Update is called once per frame
    void Update()
    {
        if (dead) { return; }
        if (stunned) { HandleStun(); }
        switch (current_state)
        {
            case States.IDLE_STATE:
                break;
            case States.STALKING_STATE:
                HandleStalking();
                break;
            case States.ATTACK_STATE:
                HandleAttack();
                break;
            default:
                break;
        }
    }
    void HandleStalking() 
    {
        float min_distance;
        Argenimal closest_enemy;
        foreach (Argenimal enemy in _enemy_team) 
        {
            
        }
    }
    void HandleAttack()
    {

    }
    void HandleStun() 
    {
        stunned_timer += Time.deltaTime;
        if (stunned_timer > MAX_STUNNED_TIMER)
        {
            stunned = false;
            stunned_timer = 0.0f;
        }
        else
        {
            return;
        }
    }
}
