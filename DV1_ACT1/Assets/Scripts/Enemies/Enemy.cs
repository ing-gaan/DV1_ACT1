using UnityEngine;
using UnityEngine.Pool;



public class Enemy : MonoBehaviour
{
    [Header("---------- Event buses")]
    [SerializeField] private TimeEventBusScrObj _timeEventBus;
    [SerializeField] private GameEventBusScrObj _gameEventBusScrObj;

    [Header("---------- Enemy")]
    [SerializeField] private float _speed = 4f;
    [SerializeField] private Transform _shootTransform;

    [Header("---------- Spawn adjusments")]
    [SerializeField] private float _minSpawnDelay = 0.5f;
    [SerializeField] private float _maxSpawnDelay = 3f;

    [Header("---------- Vertical movement adjustments")]
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


    private void OnEnable()
    {
        _timeEventBus.OneHundredMillisecondsEvent += SpawnTimer;
        _gameEventBusScrObj.OneLiveLostEvent += OneLifeLost;
    }

    private void OnDisable()
    {
        _timeEventBus.OneHundredMillisecondsEvent -= SpawnTimer;
        _gameEventBusScrObj.OneLiveLostEvent -= OneLifeLost;
    }

    private void SpawnTimer()
    {
        _timer += 0.1f;

        if (_timer > _spawnDelay)
        {
            _timer = 0;
            _spawnDelay = Random.Range(_minSpawnDelay, _maxSpawnDelay);
            _bulletSpawner.SpawnBullet(_shootTransform.position, ShootDirection.Left, typeof(BigBullet), tag);
            _audioSource.Play();
        }
    }

    private void OneLifeLost()
    {
        _enemyPool.Release(this);
    }


    private void Start()
    {        
        _audioSource = GetComponent<AudioSource>();
        _spawnDelay = Random.Range(_minSpawnDelay, _maxSpawnDelay);

        _minY = SceneController.MinY + _yRandAdjust;
        _maxY = SceneController.MaxY - _yRandAdjust;
        _yRand = Random.Range(_minY, _maxY);
        
        _yState = YState.NONE;        
    }


    private void Update()
    {
        EnemyMovement();
    }


    private void EnemyMovement()
    {
        bool goUp = _yRand >= transform.position.y;
        bool withoutDirection = _yState == YState.NONE;
        bool goingDown = _yState == YState.DOWN;
        bool goingUp = _yState == YState.UP;

        if (goUp)
        {                        
            if (withoutDirection)
            {
                _yState = YState.UP;
                _slope = Random.Range(_minSlope, _maxSlope);
            }
            else if (goingDown)
            {
                _yState = YState.NONE;
                _yRand = Random.Range(_minY, _maxY);
            }
            _velocity = new Vector3(-1, _slope, 0).normalized * _speed * Time.deltaTime;
        }
        else
        {            
            if (withoutDirection)
            {
                _yState = YState.DOWN;
                _slope = Random.Range(_minSlope, _maxSlope) * -1;
            }
            else if (goingUp)
            {
                _yState = YState.NONE;
                _yRand = Random.Range(_minY, _maxY);
            }
            _velocity = new Vector3(-1, _slope, 0).normalized * _speed * Time.deltaTime;
        }
        transform.Translate(_velocity);
        _timer += Time.deltaTime;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {     
        //if(collision.CompareTag("Boundary"))
        //{
        //    _enemyPool.Release(this);
        //}
        //else
        //{
        //    _explosionSpawner.SpawnExplosion(transform.position);
        //    _enemyPool.Release(this);
        //    _gameEventBusScrObj.RaiseEnemyKillEvent();
        //}


        GameObject collGObj = collision.gameObject;
        if (collGObj.layer == LayerMask.NameToLayer("Bullet"))
        {
            if (collGObj.GetComponent<Bullet>().TagShotIt == "Player")
            {
                _explosionSpawner.SpawnExplosion(transform.position);
                _enemyPool.Release(this);
                _gameEventBusScrObj.RaiseEnemyKillEvent();
            }
        }

        if (collision.CompareTag("Boundary"))
        {
            _enemyPool.Release(this);
        }


    }


}
