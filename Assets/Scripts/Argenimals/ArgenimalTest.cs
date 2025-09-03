using UnityEngine;

public class ArgenimalTest : MonoBehaviour
{
    [SerializeField] private ArgenimalData _argenimalData;
    [SerializeField] private BattleUI _battleUI;

    [Header("Stats Dynamic")]
    [SerializeField] private byte _level = 1;

    private void Start()
    {
        if (_argenimalData != null)
        {
            ArgenimalInstance argenimalInstance = new(_argenimalData, _level);

            Debug.Log($"{argenimalInstance.ArgenimalData.Name} - Nivel {argenimalInstance.Level}");
            Debug.Log($"Vida: {argenimalInstance.LifeCurrent}/{argenimalInstance.GetLifeMax()}");
            Debug.Log($"Ataque: {argenimalInstance.GetAttack()}");

            foreach (var move in argenimalInstance.Moves)
            {
                Debug.Log($"{_argenimalData.Name} puede usar {move.MoveData.Name}");
            }

            if (_battleUI != null)
            {
                _battleUI.SetupMoveButtons(argenimalInstance.Moves);
            }
        }
    }
}
