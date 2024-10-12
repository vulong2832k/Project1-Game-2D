using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IEnemyAnimation
{
    [Header("Enemy Data")]
    [SerializeField] protected EnemyData enemyData;
    [SerializeField] private Transform _player;
    [SerializeField] private Animator _anim;

    [SerializeField] private float _curHealth;
    private Vector3 _originalScale;
    private bool _isDamagingPlayer = false;
    private bool _isDead = false;

    public GameObject speedBoostItemPrefab;
    public GameObject ammoBoxItemPrefab;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _originalScale = transform.localScale;
        ResetEnemy();
    }
    private void OnEnable()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    // Update is called once per frame
    void Update()
    {
        MoveTowardPlayer();
    }

    private void MoveTowardPlayer()
    {
        if(_player != null && !_isDead)
        {
            Vector3 dirPlayer = (_player.position - transform.position).normalized;
            transform.position += dirPlayer * enemyData.speed * Time.deltaTime;

            float angle = Mathf.Atan2(dirPlayer.y, dirPlayer.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            if(transform.eulerAngles.z > 90 && transform.eulerAngles.z < 270)
            {
                transform.localScale = new Vector3(_originalScale.x, -_originalScale.y, _originalScale.z);
            }
            else
            {
                transform.localScale = new Vector3(_originalScale.x, _originalScale.y, _originalScale.z);
            }
        }
    }
    
    public void ResetEnemy()
    {
        _curHealth = enemyData.defaultHealth + enemyData.healthBonus;
        _isDead = false;
    }
    public void IncreaseStats(float healthIncrease, float damageIncrease)
    {
        enemyData.healthBonus += healthIncrease;
        enemyData.damageBonus += damageIncrease;
    }
    public void TakeDamage(float damage)
    {
        _curHealth -= damage;
        if (_curHealth <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        _isDead = true;
        EnemyDeadAnimation();
    }
    public void EnemyDeadAnimationComplete()
    {
        DropItemBuffSpeed();
        DropItemAmmoBox();
        gameObject.SetActive(false);
    }
    public void EnemyDeadAnimation()
    {
        if(_anim != null)
        {
            _anim.SetTrigger("isDead");
        }
    }

    private void DropItemBuffSpeed()
    {
        if(speedBoostItemPrefab != null && UnityEngine.Random.value <= enemyData.dropChangeBuffSpeedItem)
        {
            Instantiate(speedBoostItemPrefab, transform.position, Quaternion.identity);
        }
    }
    private void DropItemAmmoBox()
    {
        if(ammoBoxItemPrefab != null && UnityEngine.Random.value <= enemyData.dropChangeBuffSpeedItem)
        {
            Instantiate(ammoBoxItemPrefab, transform.position, Quaternion.identity);
        }
    }
    private IEnumerator DamagePlayer(GameObject player)
    {
        float _damageInterval = 0.5f;
        _isDamagingPlayer = true;
        while (player != null && _isDamagingPlayer)
        {
            TankController tankController = player.GetComponent<TankController>();
            if(tankController != null)
            {
                tankController.TakeDamage((int)(enemyData.damage + enemyData.damageBonus));
            }
            yield return new WaitForSeconds(_damageInterval);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!_isDamagingPlayer)
            {
                StartCoroutine(DamagePlayer(collision.gameObject));
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isDamagingPlayer = false;
        }
    }
    
}
