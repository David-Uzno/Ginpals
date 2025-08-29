using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CombatManager : MonoBehaviour
{
    public enum Teams{
        AlliedTeam,
        EnemyTeam
    }

    public Argenimal[] alliedTeam = new Argenimal[3];
    public Argenimal[] enemyTeam = new Argenimal[3];

    public GameObject text;

    private TextMeshProUGUI textWin;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartCombat()
    {
        CombatStarted(alliedTeam, enemyTeam);
        textWin = text.GetComponent<TextMeshProUGUI>();
    }
    
    void CombatStarted(Argenimal[] alliedTeam, Argenimal[] enemyTeam) 
    {
        foreach (Argenimal animal in alliedTeam)
        {
            animal.team = Teams.AlliedTeam;
            animal.enemyTeam = enemyTeam;
            animal.StartCombat();
        }
        foreach (Argenimal animal in enemyTeam) {
            animal.team = Teams.EnemyTeam;
            animal.enemyTeam = alliedTeam;
            animal.StartCombat(); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool enemyTeamDead = enemyTeam[0].IsDead() && enemyTeam[1].IsDead();
        bool alliedTeamDead = alliedTeam[0].IsDead() && alliedTeam[1].IsDead();
        if (alliedTeamDead && enemyTeamDead)
        {
            //empate
            textWin.text = "Empate!";
        }
        else if (alliedTeamDead)
        {
            //perido
            textWin.text = "Perdiste!";
        }
        else if (enemyTeamDead)
        {
            //gande
            textWin.text = "Ganaste!";
        }
        
    }
}
