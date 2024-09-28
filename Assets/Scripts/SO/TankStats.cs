using UnityEngine;

[CreateAssetMenu(fileName = "NewStats", menuName ="Tank/Stats")]
public class TankStats : ScriptableObject
{
    [Header("Start")]
    public int maxHealth = 100;
    public int damage = 1;
    public int specialSkillDamage = 20;
    public float moveSpeed = 5f;
    public int initializeAmmo = 50;
    public int currentAmmo;

    [Header("Limit")]
    public int maxAmmo = 9999;

    [HideInInspector]
    public float defaultMoveSpeed = 5f;
    public float rotationSpeed = 150f;

    private void OnEnable()
    {
        defaultMoveSpeed = moveSpeed;
        currentAmmo = initializeAmmo;
    }
    public void ResetToDefault()
    {
        moveSpeed = defaultMoveSpeed;
        initializeAmmo = 100;
    }
}
