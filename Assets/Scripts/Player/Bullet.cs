using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Setting:")]
    [SerializeField] private float _speed = 10;
    [SerializeField] private TankStats _tankStats;
    private Vector3 _direction;

    void Update()
    {
        MoveBullet();
    }
    public void SetDirection(Vector3 direction)
    {
        _direction = direction.normalized;
    }
    private void MoveBullet()
    {
        transform.Translate(_direction * _speed * Time.deltaTime, Space.World);
    }
    public void SetTankstats(TankStats tankStats)
    {
        _tankStats = tankStats;
    }
    public void SetDamage(int damage)
    {
        _tankStats.damage = damage;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
            if(enemy != null)
            {
                enemy.TakeDamage(_tankStats.damage);
            }
            gameObject.SetActive(false);
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            gameObject.SetActive(false);
        }
    }
}
