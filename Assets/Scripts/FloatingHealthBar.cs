using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthBar;

    public void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        if (currentHealth <= 0){ healthBar.value = 0;}
        healthBar.value = ((float) currentHealth) /((float) maxHealth);
    }
    

}
