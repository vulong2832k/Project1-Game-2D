using UnityEngine;

[CreateAssetMenu(fileName = "NewStats", menuName ="Tank/Stats")]
public class TankStats : ScriptableObject
{
    [Header("Stats")]
    public int maxHealth;
    public int damage;
    public int specialSkillDamage;
    public float moveSpeed;
    public int initializeAmmo;
    public int currentAmmo;
    public float fireRate;
    public int maxAmmo;

    [Header("Level")]
    public int damageLevel = 1;
    public int maxHealthLevel = 1;
    public int moveSpeedLevel = 1;
    public int fireRateLevel = 1;
    public int maxAmmoLevel = 1;

    [HideInInspector]
    public float rotationSpeed = 150f;

    private void OnEnable()
    {
        //Stats
        moveSpeed = 5f;
        currentAmmo = initializeAmmo;
        maxHealth = 100;
        damage = 1;
        fireRate = 1f;
        maxAmmo = 300;
        //Level
        damageLevel = 1; maxHealthLevel = 1; moveSpeedLevel = 1; fireRateLevel = 1; maxAmmoLevel = 1;
    }
    public void ResetToDefault()
    {
        //Stats
        moveSpeed = 5f;
        currentAmmo = initializeAmmo;
        maxHealth = 100;
        damage = 1;
        fireRate = 1f;
        maxAmmo = 300;
        //Level
        damageLevel = 1; maxHealthLevel = 1; moveSpeedLevel = 1; fireRateLevel = 1; maxAmmoLevel = 1;
    }
    public void UpgradeStat(string statType)
    {
        int maxLevel = 999;
        switch (statType)
        {
            case "Damage":
                if (damageLevel > maxLevel - 1) return;
                damageLevel++;
                damage += damageLevel * 1;
                break;
            case "MaxHealth":
                if (maxHealthLevel > maxLevel - 1) return;
                maxHealthLevel++;
                maxHealth += maxHealthLevel * 10;
                break;
            case "MoveSpeed":
                if (moveSpeedLevel > maxLevel - 1) return;
                moveSpeedLevel++;
                moveSpeed += moveSpeedLevel * 0.1f;
                break;
            case "MaxAmmo":
                if (maxAmmoLevel > maxLevel - 1) return;
                maxAmmoLevel++;
                maxAmmo += maxAmmoLevel * 15;
                break;
            case "FireRate":
                if (fireRateLevel > maxLevel - 1) return;
                fireRateLevel++;
                fireRate = Mathf.Max(fireRate - 0.005f, 0.05f);
                break;
            default: break;
        }
    }
}
