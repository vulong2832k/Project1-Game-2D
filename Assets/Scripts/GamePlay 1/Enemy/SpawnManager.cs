using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public enum EnemyType {None, Bat, Crab};

    [Header("Enemy Setting:")]
    //Spawn
    [SerializeField] private GameObject _bossPrefab;
    [SerializeField] private GameObject _crabPrefab;
    [SerializeField] private GameObject _batPrefab;
    [SerializeField] private Transform[] _spawnPoints;
    //Wave Enemy
    [SerializeField] private int _numberOfWaves = 40;
    [SerializeField] private int _enemiesPerWave = 20;
    [SerializeField] private float _spawnInterval = 1f;
    [SerializeField] private float _buffHpPerFiveWaves = 0.05f;
    [SerializeField] private float _buffDamagePerFiveWaves = 0.05f;

    [SerializeField] private EnemyData _batData;
    [SerializeField] private EnemyData _crabData;

    private int _currentWave = 0;

    void Start()
    {
        ResetEnemyData();
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        for (int wave = 0; wave <= _numberOfWaves; wave++)
        {
            _currentWave = wave;
            FindObjectOfType<UIGameplayController>().UpdateWaveText();

            if(wave == 20 || wave == 40)
            {
                SpawnBoss(wave);
            }
            else
            {
                SpawnEnemies(wave);
            }
            yield return new WaitForSeconds(_currentWave * _spawnInterval + 2f);
        }
    }

    //Spawn Enemy and Boss
    private void SpawnEnemyAtRandomPoint(EnemyType enemyType)
    {
        Transform spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
        GameObject enemy = EnemyPooling.instance.GetPooledEnemy(enemyType);
        if(enemy != null)
        {
            enemy.transform.position = spawnPoint.position;
            enemy.transform.rotation = spawnPoint.rotation;
            enemy.SetActive(true);

            EnemyController enemyController = enemy.GetComponent<EnemyController>();
            if(enemyController != null)
            {
                enemyController.ResetEnemy();

                if(_currentWave % 5 == 0 && _currentWave != 20 && _currentWave != 40)
                {
                    enemyController.IncreaseStats(_buffHpPerFiveWaves, _buffDamagePerFiveWaves);
                }
            }
        }
    }

    void SpawnEnemies(int wave)
    {
        //Spawn quái theo số lượng mình muốn trong wave
        int batCount = 0;
        int crabCount = 0;

        if (wave <= 10)
        {
            batCount = _enemiesPerWave;
        }
        else if (wave < 20)
        {
            batCount = 15;
            crabCount = 5;
        }
        else
        {
            batCount = 10;
            crabCount = 10;
        }
        //spawn loại quái theo yêu cầu
        for (int i = 0; i < crabCount; i++)
        {
            SpawnEnemyAtRandomPoint(EnemyType.Crab);
        }
        for (int i = 0; i < batCount; i++)
        {
            SpawnEnemyAtRandomPoint(EnemyType.Bat);
        }

    }
    private void SpawnBoss(int wave)
    {
        Transform spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
        GameObject boss = Instantiate(_bossPrefab, spawnPoint.position, spawnPoint.rotation);

        BossController bossController = boss.GetComponent<BossController>();
        if(bossController != null)
        {
            bossController.SetupBoss(wave);
        }
    }
    private GameObject GetEnemyPrefab(EnemyType enemyType)
    {
        switch (enemyType)
        {
            case EnemyType.Bat:
                return _batPrefab;
            case EnemyType.Crab:
                return _crabPrefab;
            default: return null;
        }
    }
    IEnumerator DelayNextSpawn()
    {
        yield return new WaitForSeconds(_spawnInterval);
    }
    public int GetCurrentWave()
    {
        return _currentWave;
    }
    public void ResetEnemyData()
    {
        _batData.healthBonus = 0;
        _batData.damageBonus = 0;

        _crabData.damageBonus = 0;
        _crabData.healthBonus = 0;
    }
}
