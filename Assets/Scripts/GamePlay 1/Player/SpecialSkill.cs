using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;


public class SpecialSkill : MonoBehaviour
{
    public TankStats tankStats;
    public UIPlayerController uiPlayerController;

    //Special Skill
    [SerializeField] protected GameObject _specialSkillPrefab;
    [SerializeField] protected Transform _skillSpawnPoint;
    [SerializeField] protected float _explosionRadius = 1f;
    [SerializeField] protected float _explosionDelay = 2f;
    [SerializeField] protected LayerMask _layerMask;

    private void Start()
    {
        StartCoroutine(HandleExplosion());
    }
    void Update()
    {

    }
    
    IEnumerator HandleExplosion()
    {
        yield return new WaitForSeconds(_explosionDelay);

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, _explosionRadius, _layerMask.value);

        foreach (Collider2D enemy in hitEnemies)
        {
            EnemyController enemyController = enemy.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                enemyController.TakeDamage(tankStats.specialSkillDamage);
            }
        }
        Destroy(gameObject);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _explosionRadius);
    }
}
