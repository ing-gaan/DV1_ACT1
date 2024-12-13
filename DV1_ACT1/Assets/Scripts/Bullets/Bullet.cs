using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Bullet : MonoBehaviour
{
    [Header("---------- Event buses")]
    [SerializeField] private GameEventBusScrObj _gameEventBusScrObj;

    [Header("---------- Bullet speed")]
    [SerializeField] private float _speed = 5.0f;


    private ObjectPool<Bullet> _bulletPool;
    public ObjectPool<Bullet> BulletPool { get => _bulletPool; set => _bulletPool = value; }

    private ShootDirection _bulletDirection;
    public ShootDirection BulletDirection { get => _bulletDirection; set => _bulletDirection = value; }

    private string _tagShotIt;
    public string TagShotIt { get => _tagShotIt; set => _tagShotIt = value; }


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
        _bulletPool.Release(this);
    }

    private void Update()
    {
        Vector3 velocity = new Vector3((int)_bulletDirection, 0, 0);
        transform.Translate(velocity * _speed * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        _bulletPool.Release(this);
    }


}
