
using Unity.Properties;
using UnityEngine;


public class Player : MonoBehaviour
{
    [Header("---------- Event buses")]
    [SerializeField] private GameEventBusScrObj _gameEventBusScrObj;

    [Header("---------- Spawners")]
    [SerializeField] private BulletSpawner _bulletSpawner;
    [SerializeField] private ExplosionSpawner _explosionSpawner;

    [Header("---------- Shoot adjustments")]
    [SerializeField] private Transform _shootPointTranform;


    private AudioSource _audioSource;
    private Vector3 _initPosition;



    private void OnEnable()
    {
        _gameEventBusScrObj.OneLiveLostEvent += OneLifeLost;
    }

    private void OnDisable()
    {
        _gameEventBusScrObj.OneLiveLostEvent -= OneLifeLost;
    }

    private void OneLifeLost()
    {
        transform.position = _initPosition;
    }


    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _initPosition = transform.position;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {            
            _bulletSpawner.SpawnBullet(_shootPointTranform.position, ShootDirection.Right, typeof(NormalBullet), tag);
            _audioSource .Play();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool isBulletOrEnemy = collision.CompareTag("Bullet") || collision.CompareTag("Enemy");
        if (isBulletOrEnemy)
        {
            _explosionSpawner.SpawnExplosion(transform.position);            
            _gameEventBusScrObj.RaiseOneLiveLostEvent();
        }
    }
}
