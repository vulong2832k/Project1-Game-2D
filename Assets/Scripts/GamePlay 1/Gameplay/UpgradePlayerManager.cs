using UnityEngine;
using UnityEngine.UI;

public class UpgradePlayerManager : MonoBehaviour
{
    [SerializeField] private TankStats _tankStats;
    [SerializeField] protected Button _upgradeDamageBtn;
    [SerializeField] protected Button _upgradeHealthBtn;
    [SerializeField] protected Button _upgradeMoveSpeedBtn;
    [SerializeField] protected Button _upgradeFireRateBtn;
    [SerializeField] protected Button _upgradeMaxAmmoBtn;

    [SerializeField] private UIShopLevelAttribute _uiShopLevelAttribute;

    // Start is called before the first frame update
    void Start()
    {
        UpgradeBtn();
    }
    private void UpgradeBtn()
    {
        _upgradeDamageBtn.onClick.AddListener(() => UpgradeStat("Damage"));
        _upgradeHealthBtn.onClick.AddListener(() => UpgradeStat("Health"));
        _upgradeMoveSpeedBtn.onClick.AddListener(() => UpgradeStat("MoveSpeed"));
        _upgradeFireRateBtn.onClick.AddListener(() => UpgradeStat("FireRate"));
        _upgradeMaxAmmoBtn.onClick.AddListener(() => UpgradeStat("MaxAmmo"));
    }
    private void UpgradeStat(string statType)
    {
        if (_tankStats != null)
        {
            _tankStats.UpgradeStat(statType);
            if(statType == "MoveSpeed")
            {
                FindObjectOfType<TankMovement>().UpdateMoveSpeed();
            }
            _uiShopLevelAttribute.UpdateUI(_tankStats);
            Debug.Log($"{statType} đã được cộng thêm 1 điểm");
            
        }
    }
    private int GetStatLevel(string statType)
    {
        switch (statType)
        {
            case "Damage": return _tankStats.damageLevel;
            case "MaxHealth": return _tankStats.maxHealthLevel;
            case "MoveSpeed": return _tankStats.moveSpeedLevel;
            case "FireRate": return _tankStats.fireRateLevel;
            case "MaxAmmo": return _tankStats.maxAmmoLevel;
            default: return 1;
        }
    }
}
