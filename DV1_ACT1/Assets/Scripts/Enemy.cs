using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;



public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 4f;
    [SerializeField] private Transform _shootTransform;
    [SerializeField] private float _minSpawnDelay = 0.5f;
    [SerializeField] private float _maxSpawnDelay = 3f;
    [SerializeField] private float _yRandAdjust = 1f;
    [Range(0.1f, 1)] [SerializeField]  private float _minSlope;
    [Range(2, 10)] [SerializeField] private float _maxSlope;


    private ObjectPool<Enemy> _enemyPool;
    public ObjectPool<Enemy> EnemyPool { get => _enemyPool; set => _enemyPool = value; }
    private BulletSpawner _bulletSpawner;
    public BulletSpawner BulletSpawner { get => _bulletSpawner; set => _bulletSpawner = value; }
    private ExplosionSpawner _explosionSpawner;
    public ExplosionSpawner ExplosionSpawner { get => _explosionSpawner; set => _explosionSpawner = value; }


    private float _timer = 0;
    private float _minY;
    private float _maxY;
    private float _yRand;
    private Vector3 _velocity;
    private bool _turnY;

    private enum YState { UP, DOWN, NONE }
    private YState _yState;
    private float _slope;
    private float _spawnDelay;
    
    private AudioSource _audioSource;


    void Start()
    {        
        _audioSource = GetComponent<AudioSource>();
        _spawnDelay = Random.Range(_minSpawnDelay, _maxSpawnDelay);

        _minY = SceneController.MinY + _yRandAdjust;
        _maxY = SceneController.MaxY - _yRandAdjust;
        _yRand = Random.Range(_minY, _maxY);
        
        _yState = YState.NONE;
        
    }


    void Update()
    {
        
        if (_yRand >= transform.position.y )
        {
            if (_yState == YState.NONE)
            {                
                _yState = YState.UP;
                _slope = Random.Range(_minSlope, _maxSlope);
            }
            else if (_yState == YState.DOWN)
            {
                _yState = YState.NONE;
                _yRand = Random.Range(_minY, _maxY);
            }
            _velocity = new Vector3(-1, _slope, 0).normalized * _speed * Time.deltaTime;
            
        }
        else
        {
            if (_yState == YState.NONE)
            {
                _yState = YState.DOWN;
                _slope = Random.Range(_minSlope, _maxSlope) * -1;
            }
            else if (_yState == YState.UP)
            {
                _yState = YState.NONE;
                _yRand = Random.Range(_minY, _maxY);
            }
            _velocity = new Vector3(-1, _slope, 0).normalized * _speed * Time.deltaTime;            
        }


        transform.Translate(_velocity);
        _timer += Time.deltaTime;

        if(_timer > _spawnDelay)
        {
            _timer = 0;
            _spawnDelay = Random.Range(_minSpawnDelay, _maxSpawnDelay);
            _bulletSpawner.SpawnBullet(_shootTransform.position, ShootDirection.Left, typeof(BigBullet), tag);
            _audioSource.Play();
        }


    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collGObj = collision.gameObject;
        if (collGObj.layer == LayerMask.NameToLayer("Bullet"))
        {
            if(collGObj.GetComponent<Bullet>().TagShotIt == "Player")
            {                
                _explosionSpawner.SpawnExplosion(transform.position);
                _enemyPool.Release(this);                
            }            
        }
        
        if(collision.CompareTag("Boundary"))
        {
            _enemyPool.Release(this);
        }


    }


}
