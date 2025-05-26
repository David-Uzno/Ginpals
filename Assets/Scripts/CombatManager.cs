using UnityEngine;

public class CombatManager : MonoBehaviour
{
    enum Teams{
        AlliedTeam,
        EnemyTeam
    }

    public Argenimal[] alliedTeam = new Argenimal[3];
    public Argenimal[] enemyTeam = new Argenimal[3];
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CombatStarted(alliedTeam, enemyTeam);
 
    }
    void CombatStarted(Argenimal[] alliedTeam, Argenimal[] enemyTeam) 
    {
        foreach (Argenimal animal in alliedTeam) { animal.StartCombat(); }
        foreach (Argenimal animal in enemyTeam) { animal.StartCombat(); }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
