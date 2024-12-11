using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 4.0f;

    private BulletSpawner _bulletSpawner;
    public BulletSpawner BulletSpawner { get => _bulletSpawner; set => _bulletSpawner = value; }


    private float _timer = 0;

    void Start()
    {
        
    }


    void Update()
    {
        transform.Translate(Vector3.left * _speed * Time.deltaTime);

        _timer += Time.deltaTime;

        if(_timer > 1)
        {
            _timer = 0;
            _bulletSpawner.SpawnBullet(transform.position, ShootDirection.Left);
        }

    }
}
