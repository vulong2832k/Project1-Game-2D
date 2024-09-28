using System.Collections.Generic;
using UnityEngine;

public class PoolingBullet : MonoBehaviour
{
    public static PoolingBullet instance;
    [SerializeField] private GameObject _bulletPrefabs;
    private List<GameObject> _bullets = new List<GameObject>();
    private int _amountBullets = 30;

    private GameObject _listPoolBullet;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        SettingListBulletPool();
    }
    private void SettingListBulletPool()
    {
        _listPoolBullet = new GameObject("ListPoolBullet");

        for (int i = 0; i < _amountBullets; i++)
        {
            GameObject obj = Instantiate(_bulletPrefabs);
            obj.transform.SetParent(_listPoolBullet.transform);
            obj.SetActive(false);
            _bullets.Add(obj);
        }
    }
    public GameObject GetBulletPooledObject()
    {
        for (int i = 0; i < _bullets.Count; i++)
        {
            if (!_bullets[i].activeInHierarchy)
            {
                return _bullets[i];
            }
        }
        return null;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            gameObject.SetActive(false);
        }
    }
}
