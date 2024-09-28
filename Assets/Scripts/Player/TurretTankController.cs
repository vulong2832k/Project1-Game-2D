using System;
using System.Collections;
using UnityEngine;

public class TurretTankController : MonoBehaviour
{
    public Transform firePoint;
    public TankStats tankStats;
    public UIPlayerController uiPlayerController;
    public GameObject specialSkill;

    //auto fire
    private bool _isAutoFire = false;
    private Transform _curTarget;

    //Properties
    private float _fireRate = 0.2f;
    private float _nextFireTime = 0;

    //RotateTurret
    private float _rotationSpeed = 500f;

    //Update Target Interval
    private float _targetUpdateInterval = 1f;
    private float _nextTargetUpdateTime = 0;

    //Special Skill
    private float _specialSkillCooldown = 30f;
    private float _currentCooldownTime = 0;
    private bool _skillOnCoolDown = false;
    private void Start()
    {
        uiPlayerController.UpdateAmmo(tankStats.currentAmmo, tankStats.maxAmmo);
    }
    private void Update()
    {
        SettingAutoFire();
        UseSpecialSkill();
    }
    private void SettingAutoFire()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            _isAutoFire = !_isAutoFire;
        }
        if (_isAutoFire)
        {
            AutoTargetAndFire();
        }
        else
        {
            RotateTurret();
            FireBullet();
        }
    }
    //xoay súng
    private void RotateTurret()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        Vector3 dir = (mousePos - transform.position).normalized;
        float targetAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        float currentAngle = Mathf.LerpAngle(transform.eulerAngles.z, targetAngle - 90f, _rotationSpeed * Time.deltaTime);

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, currentAngle));
    }
    //bắn
    void FireBullet()
    {
        if (Input.GetMouseButton(0) && !_isAutoFire && Time.time >= _nextFireTime)
        {
            if (tankStats.currentAmmo > 0)
            {
                Shoot();
                UseAmmo(1);
                uiPlayerController.UpdateAmmo(tankStats.currentAmmo, tankStats.maxAmmo);
                _nextFireTime = Time.time + _fireRate;
            }

        }
    }
    void Shoot()
    {
        GameObject bullet = PoolingBullet.instance.GetBulletPooledObject();
        Bullet bulletScript = bullet.GetComponent<Bullet>();

        if (bulletScript != null)
        {
            bulletScript.transform.position = firePoint.position;
            bulletScript.transform.rotation = firePoint.rotation;
            bullet.SetActive(true);

            bulletScript.SetDirection(firePoint.up);
            bulletScript.SetDamage(tankStats.damage);
        }
    }
    //Tự động bắn
    private void AutoTargetAndFire()
    {
        if (Time.time >= _nextTargetUpdateTime)
        {
            FindNearestTarget();
            _nextTargetUpdateTime = Time.time + _targetUpdateInterval;
        }
        if (_curTarget != null)
        {
            Vector3 dir = _curTarget.position - transform.position;
            float targetAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, targetAngle - 90f));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }
        if (_curTarget != null && Time.time > _nextFireTime)
        {
            if (tankStats.currentAmmo > 0)
            {
                Shoot();
                UseAmmo(1);
                uiPlayerController.UpdateAmmo(tankStats.currentAmmo, tankStats.maxAmmo);
                _nextFireTime = Time.time + _fireRate;
            }

        }
    }
    //Check Enemy Position
    private void FindNearestTarget()
    {
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        if (nearestEnemy != null && shortestDistance <= 25f)
        {
            _curTarget = nearestEnemy.transform;
        }
        else
        {
            _curTarget = null;
        }
    }
    //Ammo Controller
    private void UseAmmo(int amount)
    {
        if(tankStats.currentAmmo - amount < 0)
        {
            tankStats.currentAmmo = 0;
        }
        else
        {
            tankStats.currentAmmo -= amount;
        }
    }
    //Special Skill
    private void UseSpecialSkill()
    {
        if (_skillOnCoolDown)
        {
            _currentCooldownTime -= Time.deltaTime;
            uiPlayerController.UpdateReadySkill(_currentCooldownTime, _specialSkillCooldown);

            if (_currentCooldownTime <= 0)
            {
                _skillOnCoolDown = false;
                _currentCooldownTime = 0;
                uiPlayerController.UpdateReadySkill(0, _specialSkillCooldown);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && !_skillOnCoolDown)
        {
            ActiveSpecialSkill();
            _skillOnCoolDown = true;
            _currentCooldownTime = _specialSkillCooldown;
        }
    }

    private void ActiveSpecialSkill()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        GameObject CreateSpecialSkill = Instantiate(specialSkill, mousePos, Quaternion.identity);
    }
}
