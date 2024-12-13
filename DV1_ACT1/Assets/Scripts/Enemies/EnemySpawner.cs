using UnityEngine;
using UnityEngine.Pool;


public class EnemySpawner : MonoBehaviour
{
    [Header("---------- Event buses")]
    [SerializeField] private TimeEventBusScrObj _timeEventBus;

    [Header("---------- Prefabs")]
    [SerializeField] private Enemy _enemyPrefab;

    [Header("---------- Spawners")]
    [SerializeField] private BulletSpawner _bulletSpawner;
    [SerializeField] private ExplosionSpawner _explosionSpawner;

    [Header("---------- Spawn adjustments")]
    [SerializeField] private float _xSpawnAdjust = 1f;
    [SerializeField] private float _ySpawnAdjust = 1f;
    [SerializeField] private float _minSpawnDelay = 0.5f;
    [SerializeField] private float _maxSpawnDelay = 3f;


    private ObjectPool<Enemy> _enemyPool;

    private float _timer = 0f;
    private float _xSpawnPos = 0f;
    private float ySpawnPos = 0f;
    private float _spawnDelay = 0f;


    

    private void OnEnable()
    {
        _timeEventBus.OneHundredMillisecondsEvent += SpawnTimer;
    }

    private void OnDisable()
    {
        _timeEventBus.OneHundredMillisecondsEvent -= SpawnTimer;
    }

    private void SpawnTimer()
    {
        _timer += 0.1f;

        if (_timer > _spawnDelay)
        {
            _timer = 0;
            SetSpawnDelay();
            _enemyPool.Get();
        }
    }


    private void Awake()
    {
        _enemyPool = new ObjectPool<Enemy>(CreateEnemy, GetEnemy, ReleaseEnemy, DestroyEnemy);
    }

    private void Start()
    {
        _xSpawnPos = SceneController.MaxX + _xSpawnAdjust;
        SetSpawnDelay();
    }
      


    private void SetSpawnDelay()
    {
        _spawnDelay = Random.Range(_minSpawnDelay, _maxSpawnDelay);
    }



    private Enemy CreateEnemy()
    {
        Enemy enemy = Instantiate(_enemyPrefab, SetSpawnPosition(), Quaternion.identity);
        enemy.EnemyPool = _enemyPool;
        enemy.BulletSpawner = _bulletSpawner;
        enemy.ExplosionSpawner = _explosionSpawner;
        return enemy;
    }

    private Vector3 SetSpawnPosition()
    {
        float minRand = SceneController.MinY + _ySpawnAdjust;
        float maxRand = SceneController.MaxY - _ySpawnAdjust;
        ySpawnPos = Random.Range(minRand, maxRand);

        return new Vector3(_xSpawnPos, ySpawnPos, 0);
    }

    private void GetEnemy(Enemy enemy)
    {
        //enemy.BulletDirection = _shootDirection;
        
        enemy.transform.position = SetSpawnPosition();
        enemy.gameObject.SetActive(true);
    }
        

    private void ReleaseEnemy(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
    }

    private void DestroyEnemy(Enemy enemy)
    {
        Destroy(enemy.gameObject);
    }


    
}
