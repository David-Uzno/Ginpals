using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private BattleUI _battleUI;

    private void Awake()
    {
        if (_battleUI == null)
        {
            _battleUI = Object.FindAnyObjectByType<BattleUI>();

            if (_battleUI == null)
            {
                Debug.LogError("BattleUI no se encontró en la escena y no está asignado en el inspector.");
            }
        }
    }

    private void Start()
    {
        _battleUI.MoveSelectedEvent += HandleMoveSelected;
    }

    private void HandleMoveSelected(MoveInstance move)
    {
        Debug.Log($"Se seleccionó {move.MoveData.Name} | Potencia: {move.MoveData.Power}, Espera: {move.MoveData.Wait}");
    }
}
