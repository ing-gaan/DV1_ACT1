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





    void Start()
    {
        
    }


    void Update()
    {
        Vector3 velocity = new Vector3((int)_bulletDirection, 0, 0);
        transform.Translate(velocity * _speed * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        _bulletPool.Release(this);
    }



}
