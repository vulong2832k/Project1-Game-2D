using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPooling : MonoBehaviour
{
    public static EnemyPooling instance;

    [Header("Setting:")]
    [SerializeField] private List<GameObject> _enemyPrefabs;
    [SerializeField] private int _poolSize;

    protected Dictionary<SpawnManager.EnemyType, List<GameObject>> poolDictionary = new Dictionary<SpawnManager.EnemyType, List<GameObject>>();

    private GameObject _listPoolEnemy;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        InitializePool();
    }
    void InitializePool()
    {
        _listPoolEnemy = new GameObject("ListPoolEnemy");

        foreach (GameObject prefab in _enemyPrefabs)
        {
            SpawnManager.EnemyType enemyType = GetEnemyTypeFromPrefab(prefab);
            if (enemyType != SpawnManager.EnemyType.None)
            {
                List<GameObject> objectPool = new List<GameObject>();
                for(int i = 0; i < _poolSize; i++)
                {
                    GameObject enemy = Instantiate(prefab);
                    enemy.transform.SetParent(_listPoolEnemy.transform);
                    enemy.SetActive(false);
                    objectPool.Add(enemy);
                }
                poolDictionary.Add(enemyType, objectPool);
            }
            
        }
    }
    public GameObject GetPooledEnemy(SpawnManager.EnemyType enemyType)
    {
        if (poolDictionary.ContainsKey(enemyType))
        {
            foreach (GameObject enemy in poolDictionary[enemyType])
            {
                if (!enemy.activeInHierarchy)
                {
                    return enemy;
                }
            }
            
        }
        return null;
    }
    private SpawnManager.EnemyType GetEnemyTypeFromPrefab(GameObject prefab)
    {
        if (prefab.name.Contains("Bat"))
        {
            return SpawnManager.EnemyType.Bat;
        }
        if (prefab.name.Contains("Crab"))
        {
            return SpawnManager.EnemyType.Crab;
        }
        return SpawnManager.EnemyType.None;
    }
}
