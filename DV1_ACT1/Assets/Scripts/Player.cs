    using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField] private BulletSpawner _bulletSpawner;
    [SerializeField] private Transform _shootPointTranform;

    private AudioSource _audioSource;


    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {            
            _bulletSpawner.SpawnBullet(_shootPointTranform.position, ShootDirection.Right, typeof(NormalBullet), tag);
            _audioSource .Play();
        }
    }
}
