using UnityEngine;

public class TankController : MonoBehaviour
{
    [SerializeField] protected TankStats tankStats;

    [Header("Setting:")]
    [SerializeField] private int _curHealth;

    public UIPlayerController uiPlayerController;
    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _curHealth = tankStats.maxHealth;
        _gameManager = FindObjectOfType<GameManager>();
        if(_gameManager == null)
        {
            Debug.LogError("Không tìm thấy GameManager");
        }
        
    }
    public void TakeDamage(int damage)
    {
        _curHealth -= damage;
        uiPlayerController.UpdateBar(_curHealth, tankStats.maxHealth);
        if (_curHealth <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        gameObject.SetActive(false);
        _gameManager.PlayerDied();
    }

    public void AddAmmoFromItemDrop(int amount)
    {
        Debug.Log("Adding ammo: " + amount);
        if (tankStats.currentAmmo + amount > tankStats.maxAmmo)
        {
            tankStats.currentAmmo = tankStats.maxAmmo;
        }
        else
        {
            tankStats.currentAmmo += amount;
        }
        uiPlayerController.UpdateAmmo(tankStats.currentAmmo, tankStats.maxAmmo);
        Debug.Log("Ammo added. Current ammo: " + tankStats.currentAmmo);
    }

}
