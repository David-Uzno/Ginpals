using TMPro;
using UnityEngine;

public class QualityOption : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _dropDownQuality;
    [SerializeField] private int _quality;

    private void Start()
    {
        _quality = PlayerPrefs.GetInt("QualityNumber", 3);
        _dropDownQuality.value = _quality;
        AdjustQuality();
    }

    public void AdjustQuality()
    {
        QualitySettings.SetQualityLevel(_dropDownQuality.value);
        PlayerPrefs.SetInt("QualityNumber", _dropDownQuality.value);
        _quality = _dropDownQuality.value;
    }
}