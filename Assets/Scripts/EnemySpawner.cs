using UnityEngine;

public class EnemySpawner : ObjectPool
{
    [SerializeField] private GameObject[] _enemyPrefabs;
    [SerializeField] private Vector3[] _enemySpawnPosition;
    [SerializeField] private float _secondsBetweenSpawn;
    [SerializeField] private Transform _targetChase;
    [SerializeField] private LevelHandler _levelHandler;

    private float _elapsedTime;

    private void Start()
    {
        Init(_enemyPrefabs[0], _targetChase);
    }

    private void Update()
    {
        if(_levelHandler.IsLevelStarted)
        {
            _elapsedTime += Time.deltaTime;
            if (_elapsedTime >= _secondsBetweenSpawn)
            {
                if (TryGetObject(out GameObject enemy))
                {
                    int spawnPoint = Random.Range(0, _enemySpawnPosition.Length);
                    SetEnemy(enemy, _enemySpawnPosition[spawnPoint]);
                    _elapsedTime = 0;
                }
            }
        }
    }

    private void SetEnemy(GameObject enemy, Vector3 spawnPosition)
    {
        enemy.SetActive(true);
        enemy.transform.position = spawnPosition;
        enemy.GetComponent<Enemy>().Init();
    }
}
