using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField] private Slider _healthBar;

    public void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        if (currentHealth <= 0){ _healthBar.value = 0;}
        _healthBar.value = ((float) currentHealth) /((float) maxHealth);
    }
}
