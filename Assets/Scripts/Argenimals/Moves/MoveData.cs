using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMove", menuName = "Argenimals/MoveData")]
public class MoveData : ScriptableObject
{
    [Header("Information")]
    public string Name;
    public string Description;
    public List<ArgenimalType> Type;

    [Header("Stats")]
    public int Power;
    public byte Wait;
}
