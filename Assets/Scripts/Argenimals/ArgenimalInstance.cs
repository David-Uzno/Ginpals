using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ArgenimalInstance
{
    #region Variables
    public ArgenimalData ArgenimalData { get; private set; }

    [Header("Stats")]
    public int Level { get; private set; }
    public int LifeCurrent { get; set; }

    [Header("Moves")]
    public List<MoveInstance> Moves { get; private set; } = new();

    public int GetLifeMax() => Mathf.FloorToInt(ArgenimalData.Life * Level);
    public int GetAttack() => Mathf.FloorToInt(ArgenimalData.Attack * Level);
    #endregion

    #region Constructor
    public ArgenimalInstance(ArgenimalData argenimalData, int level)
    {
        ArgenimalData = argenimalData;
        Level = level;
        LifeCurrent = GetLifeMax(); // Empieza con vida llena

        GenerateMoves();
    }
    #endregion

    #region Moves
    private void GenerateMoves()
    {
        if (ArgenimalData.AvailableMoves.Count == 0)
        {
            Debug.LogWarning($"{ArgenimalData.Name} no tiene movimientos disponibles.");
            return;
        }

        // Creamos una lista solo con movimientos únicos
        List<MoveData> uniqueMoves = ArgenimalData.AvailableMoves.Distinct().ToList();

        // Número máximo de movimientos = 2 o la cantidad disponible, lo que sea menor
        int maxMoves = Mathf.Min(2, uniqueMoves.Count);
        int numMoves = Random.Range(1, maxMoves + 1); // Random.Range exclusivo del límite superior en int

        // Copia de la lista de movimientos únicos
        List<MoveData> availableMovesPool = new List<MoveData>(uniqueMoves);

        for (int i = 0; i < numMoves; i++)
        {
            int moveIndex = Random.Range(0, availableMovesPool.Count);
            MoveData move = availableMovesPool[moveIndex];
            availableMovesPool.RemoveAt(moveIndex); // evitar repetidos

            Moves.Add(new MoveInstance(move));
        }
    }
    #endregion
}
