    using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField] private BulletSpawner _bulletSpawner;



    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {            
            _bulletSpawner.SpawnBullet(transform.position, ShootDirection.Right, typeof(NormalBullet));
        }
    }
}
