using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;



public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 4.0f;
    [SerializeField] private Transform _shootTransform;
    [SerializeField] private float _minShootDelay = 0.5f;
    [SerializeField] private float _maxShootDelay = 3.0f;
    [SerializeField] private float _yRandAdjust = 1.0f;

    private ObjectPool<Enemy> _enemyPool;
    public ObjectPool<Enemy> EnemyPool { get => _enemyPool; set => _enemyPool = value; }
    private BulletSpawner _bulletSpawner;
    public BulletSpawner BulletSpawner { get => _bulletSpawner; set => _bulletSpawner = value; }


    private float _timer = 0;
    private float _minY;
    private float _maxY;
    private float _yRand;
    private Vector3 _velocity;
    private bool _turnY;

    private enum YState { UP, DOWN, NONE }
    private YState _yState;

    void Start()
    {
        _timer = Random.Range(_minShootDelay, _maxShootDelay);

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
            }
            else if (_yState == YState.DOWN)
            {
                _yState = YState.NONE;
                _yRand = Random.Range(_minY, _maxY);
            }

            _velocity = new Vector3(-1, 1, 0).normalized * _speed * Time.deltaTime;
            
        }
        else
        {
            if (_yState == YState.NONE)
            {
                _yState = YState.DOWN;
            }
            else if (_yState == YState.UP)
            {
                _yState = YState.NONE;
                _yRand = Random.Range(_minY, _maxY);
            }
            _velocity = new Vector3(-1, -1, 0).normalized * _speed * Time.deltaTime;            
        }


        transform.Translate(_velocity);
        _timer += Time.deltaTime;

        if(_timer > _maxShootDelay)
        {
            _timer = Random.Range(_minShootDelay, _maxShootDelay);
            _bulletSpawner.SpawnBullet(_shootTransform.position, ShootDirection.Left, typeof(BigBullet));
        }

        //if(transform.position.y)
        //{

        //}

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        _enemyPool.Release(this);
    }


}
