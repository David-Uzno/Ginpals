using UnityEngine;
using UnityEngine.UI;

public class BrightnessOption : MonoBehaviour
{
    [SerializeField] private Slider _sliderBrightness;
    [SerializeField] private Image _panelBrightness;

    [SerializeField] private float _valueBrightness;

    private void Start()
    {
        _sliderBrightness.value = PlayerPrefs.GetFloat("Brightness", 5);
        _valueBrightness = _sliderBrightness.value;
    }

    private void Update()
    {
        // Oscurecer.
        if (_valueBrightness < 5)
        {
            _panelBrightness.color = new Color(0, 0, 0, 1 - _valueBrightness / 5);
        }

        // Iluminar.
        else
        {
            _panelBrightness.color = new Color(1, 1, 1, (_valueBrightness - 5) / 5);
        }
    }

    public void ChangeSlider(float value)
    {
        _valueBrightness = value;
        PlayerPrefs.SetFloat("Brightness", _valueBrightness);
    }
}