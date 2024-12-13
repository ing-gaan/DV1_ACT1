using System;
using UnityEngine;
using UnityEngine.Pool;

public class BulletSpawner : MonoBehaviour
{
    
    [Header("---------- Bullet prefabs")]
    [SerializeField] private Bullet _littleBulletPrefab;
    [SerializeField] private Bullet _normalBulletPrefab;
    [SerializeField] private Bullet _bigBulletPrefab;
    

    private ObjectPool<Bullet> _littleBulletPool;
    private ObjectPool<Bullet> _normalBulletPool;
    private ObjectPool<Bullet> _bigBulletPool;


    private ShootDirection _shootDirection;
    private Vector3 _spawnPosition;
    private Type _bulletType;
    private Bullet _bulletPrefab;
    private ObjectPool<Bullet> _bulletPool;
    private string _tagShotIt;


    
    void Awake()
    {
        _littleBulletPool = new ObjectPool<Bullet>(CreateBullet, GetBullet, ReleaseBullet, DestroyBullet);
        _normalBulletPool = new ObjectPool<Bullet>(CreateBullet, GetBullet, ReleaseBullet, DestroyBullet);
        _bigBulletPool = new ObjectPool<Bullet>(CreateBullet, GetBullet, ReleaseBullet, DestroyBullet);
    }


    public void SpawnBullet(Vector3 spawnPosition, ShootDirection shootDirection, Type bulletType, string tagShotIt)
    {
        _spawnPosition = spawnPosition;
        _shootDirection = shootDirection;
        _bulletType = bulletType;
        _tagShotIt = tagShotIt;

        if (bulletType == typeof(BigBullet))
        {
            _bulletPrefab = _bigBulletPrefab;
            _bulletPool = _bigBulletPool;
            
        }
        else if (bulletType == typeof(LittleBullet))
        {
            _bulletPrefab = _littleBulletPrefab;
            _bulletPool = _littleBulletPool;
        }
        else 
        {
            _bulletPrefab = _normalBulletPrefab;
            _bulletPool = _normalBulletPool;
        }
        _bulletPool.Get();
    }

    private Bullet CreateBullet()
    {
        Bullet bullet = Instantiate(_bulletPrefab, _spawnPosition, Quaternion.identity);
        bullet.BulletPool = _bulletPool;
        return bullet;
    }
    private void GetBullet(Bullet bullet)
    {
        bullet.BulletDirection = _shootDirection;
        bullet.transform.position = _spawnPosition;
        bullet.TagShotIt = _tagShotIt;
        bullet.gameObject.SetActive(true);
    }
    private void ReleaseBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }
    private void DestroyBullet(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }
}



public enum ShootDirection
{
    Left = -1,
    Right = 1,
    NoDirection = 0,
}
