using UnityEngine;
using TMPro;

public class UIShopLevelAttribute : MonoBehaviour
{
    [Header("Attribute Text")]
    [SerializeField] private TextMeshProUGUI _damageText;
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private TextMeshProUGUI _moveSpeedText;
    [SerializeField] private TextMeshProUGUI _maxAmmoText;
    [SerializeField] private TextMeshProUGUI _fireRateText;

    public void UpdateUI(TankStats _tankStats)
    {
        _damageText.text = _tankStats.damageLevel.ToString("D3") + " / 999";
        _healthText.text = _tankStats.maxHealthLevel.ToString("D3") + " / 999";
        _moveSpeedText.text = _tankStats.moveSpeedLevel.ToString("D3") + " / 999";
        _fireRateText.text = _tankStats.fireRateLevel.ToString("D3") + " / 999";
        _maxAmmoText.text = _tankStats.maxAmmoLevel.ToString("D3") + " / 999";
    }
}
