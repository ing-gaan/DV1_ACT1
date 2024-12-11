using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;

    private ObjectPool<Bullet> _bulletPool;
    private ShootDirection _shootDirection;
    private Vector3 _spawnPosition;

    void Awake()
    {
        _bulletPool = new ObjectPool<Bullet>(CreateBullet, GetBullet, ReleaseBullet, DestroyBullet);

    }


    void Start()
    {
        
    }

    
    void Update()
    {
        
    }


    public void SpawnBullet(Vector3 spawnPosition, ShootDirection shootDirection)
    {
        _spawnPosition = spawnPosition;
        _shootDirection = shootDirection;
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
        bullet.gameObject.SetActive(true);
    }
    private void ReleaseBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }
    private void DestroyBullet(Bullet bullet)
    {
        throw new NotImplementedException();
    }
}



public enum ShootDirection
{
    Left = -1,
    Right = 1,
    NoDirection = 0,
}
