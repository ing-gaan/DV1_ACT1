using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using static UnityEngine.EventSystems.EventTrigger;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private BulletSpawner _bulletSpawner;
    [SerializeField] private ExplosionSpawner _explosionSpawner;
    [SerializeField] private float _xSpawnAdjust = 1f;
    [SerializeField] private float _ySpawnAdjust = 1f;
    [SerializeField] private float _minSpawnDelay = 0.5f;
    [SerializeField] private float _maxSpawnDelay = 3f;

    private ObjectPool<Enemy> _enemyPool;

    private float _timer = 0;
    private float _xSpawnPos;
    private float ySpawnPos;
    private float _spawnDelay;


    private void Awake()
    {
        _enemyPool = new ObjectPool<Enemy>(CreateEnemy, GetEnemy, ReleaseEnemy, DestroyEnemy);
    }


    void Start()
    {
        _xSpawnPos = SceneController.MaxX + _xSpawnAdjust;
    }



    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > _spawnDelay)
        {
            _timer = 0;
            _spawnDelay = Random.Range(_minSpawnDelay, _maxSpawnDelay);
            _enemyPool.Get();
        }
    }


    private Enemy CreateEnemy()
    {
        Enemy enemy = Instantiate(_enemyPrefab, SetSpawnPosition(), Quaternion.identity);
        enemy.EnemyPool = _enemyPool;
        enemy.BulletSpawner = _bulletSpawner;
        enemy.ExplosionSpawner = _explosionSpawner;
        return enemy;
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


    private Vector3 SetSpawnPosition()
    {
        float minRand = SceneController.MinY + _ySpawnAdjust;
        float maxRand = SceneController.MaxY - _ySpawnAdjust;
        ySpawnPos = Random.Range(minRand, maxRand);

        return new Vector3(_xSpawnPos, ySpawnPos, 0);
    }
}
