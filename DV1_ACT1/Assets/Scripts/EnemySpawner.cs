using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private BulletSpawner _bulletSpawner;

    private float _timer;




    void Start()
    {
        
    }



    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > 4)
        {
            _timer = 0;
            Enemy enemy = Instantiate(_enemyPrefab, Vector3.right * 10, Quaternion.identity);
            enemy.BulletSpawner = _bulletSpawner;

        }
    }
}
