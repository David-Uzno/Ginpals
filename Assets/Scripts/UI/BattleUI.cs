using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    [SerializeField] private List<Button> moveButtons = new();
    private List<TMP_Text> moveTexts = new();

    public delegate void OnMoveSelected(MoveInstance move);
    public event OnMoveSelected MoveSelectedEvent;

    private void Awake()
    {
        // Asignar automáticamente moveTexts basándose en moveButtons
        moveTexts = new List<TMP_Text>();
        foreach (var button in moveButtons)
        {
            TMP_Text textComponent = button.GetComponentInChildren<TMP_Text>();
            if (textComponent != null)
            {
                moveTexts.Add(textComponent);
            }
        }
    }

    public void SetupMoveButtons(List<MoveInstance> argenimalMoves)
    {
        // Desactivar todos los botones primero
        for (int i = 0; i < moveButtons.Count; i++)
        {
            moveButtons[i].gameObject.SetActive(false);
        }

        // Activar solo los botones necesarios y asignar el nombre del movimiento
        int count = Mathf.Min(argenimalMoves.Count, moveButtons.Count);

        for (int i = 0; i < count; i++)
        {
            moveButtons[i].gameObject.SetActive(true);
            moveTexts[i].text = argenimalMoves[i].MoveData.Name;

            int index = i;
            moveButtons[i].onClick.AddListener(() => OnMoveButtonClicked(argenimalMoves[index]));
        }
    }

    private void OnMoveButtonClicked(MoveInstance move)
    {
        if (MoveSelectedEvent != null)
        {
            MoveSelectedEvent(move);
        }
    }
}
