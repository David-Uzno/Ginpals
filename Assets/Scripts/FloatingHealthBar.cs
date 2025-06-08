using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthBar;

    public void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        healthBar.value = ((float) currentHealth) /((float) maxHealth);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
