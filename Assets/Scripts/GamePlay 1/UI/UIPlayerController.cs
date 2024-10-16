using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIPlayerController : MonoBehaviour
{
    [SerializeField] private TankStats _tankstats;
    //Health
    [Header("Health:")]
    [SerializeField] private Image _healthImg;
    [SerializeField] private TextMeshProUGUI _healthText;

    private void Start()
    {
        UpdateAmmo(_tankstats.currentAmmo, _tankstats.maxAmmo);
    }

    public void UpdateBar(int curValue, int maxValue)
    {
        _healthImg.fillAmount = (float)curValue / (float)maxValue;
        _healthText.text = curValue.ToString() + "%";
    }

    //Ammo
    [Header("Ammo:")]
    [SerializeField] private TextMeshProUGUI _ammoText;

    public void UpdateAmmo(int curAmmo, int maxAmmo)
    {
        Debug.Log("UpdateAmmo called with curAmmo: " + curAmmo + ", maxAmmo: " + maxAmmo);
        _ammoText.text = curAmmo.ToString() + " / " + maxAmmo.ToString();
    }

    //SpecialSkill
    [Header("Skill")]
    [SerializeField] private Image _reloadSkillImg;
    
    public void UpdateReadySkill(float reload, float totalCooldown)
    {
        _reloadSkillImg.fillAmount = 1 - (reload / totalCooldown);
    } 
}
