using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 5.0f;
    private ObjectPool<Bullet> _bulletPool;
    public ObjectPool<Bullet> BulletPool { get => _bulletPool; set => _bulletPool = value; }

    private ShootDirection _bulletDirection;
    public ShootDirection BulletDirection { get => _bulletDirection; set => _bulletDirection = value; }


    private float _timer;
    
    



    void Start()
    {
        
    }


    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > 4.0f)
        {
            _timer = 0;
            _bulletPool.Release(this);
        }
        Vector3 velocity = new Vector3((int)_bulletDirection, 0, 0);
        transform.Translate(velocity * _speed * Time.deltaTime);
    }
}
