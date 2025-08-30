using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CombatManager : MonoBehaviour
{
    public enum Teams
    {
        AlliedTeam,
        EnemyTeam
    }

    [Header("Combat")]
    public Argenimal[] alliedTeam = new Argenimal[3];
    public Argenimal[] enemyTeam = new Argenimal[3];

    [Header("UI")]
    [SerializeField] private GameObject _winnerUI;
    public GameObject text;
    private TextMeshProUGUI textWin;

    public void StartCombat()
    {
        CombatStarted(alliedTeam, enemyTeam);
        textWin = text.GetComponent<TextMeshProUGUI>();
    }

    private void CombatStarted(Argenimal[] alliedTeam, Argenimal[] enemyTeam)
    {
        foreach (Argenimal animal in alliedTeam)
        {
            animal.team = Teams.AlliedTeam;
            animal.enemyTeam = enemyTeam;
            animal.StartCombat();
        }
        foreach (Argenimal animal in enemyTeam)
        {
            animal.team = Teams.EnemyTeam;
            animal.enemyTeam = alliedTeam;
            animal.StartCombat();
        }
    }

    private void Update()
    {
        bool enemyTeamDead = enemyTeam[0].IsDead() && enemyTeam[1].IsDead();
        bool alliedTeamDead = alliedTeam[0].IsDead() && alliedTeam[1].IsDead();

        if (alliedTeamDead && enemyTeamDead)
        {
            textWin.text = "EMPATE";
        }
        else if (alliedTeamDead)
        {
            textWin.text = "DERROTA";
        }
        else if (enemyTeamDead)
        {
            textWin.text = "VICTORIA";
            if (_winnerUI != null)
            {
                _winnerUI.SetActive(true);
            }
        }
    }

    public void Overworld()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
