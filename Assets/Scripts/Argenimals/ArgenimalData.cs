using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewArgenimal", menuName = "Argenimals/ArgenimalData")]
public class ArgenimalData : ScriptableObject
{
    [Header("Information")]
    public string Name;
    public Sprite Sprite;
    public string Description;
    public List<ArgenimalType> Type;

    [Header("Stats")]
    public byte Life = 1;
    public byte Attack = 1;
    public byte AttackSpeed = 0;
    public byte MoveSpeed = 1;
    public byte Defense = 0;

    [Header("Moves")]
    public List<MoveData> AvailableMoves;
}
