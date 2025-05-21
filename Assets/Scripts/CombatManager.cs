using System;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    enum TEAMS{
        RED_BLACK_TEAM,
        BLUE_YELLOW_TEAM
    }
    //team newbel
    public Argenimal[] red_black_team = new Argenimal[3];
    public Argenimal[] blue_yellow_team = new Argenimal[3];
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CombatStarted(red_black_team, blue_yellow_team);
 
    }
    void CombatStarted(Argenimal[] team_1, Argenimal[] team_2) 
    {
        foreach (Argenimal animal in team_1) { animal.StartCombat(team_2); }
        foreach (Argenimal animal in team_2) { animal.StartCombat(team_1); }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
