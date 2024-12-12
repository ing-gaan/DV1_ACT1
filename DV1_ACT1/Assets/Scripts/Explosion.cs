using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Explosion : MonoBehaviour
{

    private ObjectPool<Explosion> _explosionPool;
    public ObjectPool<Explosion> ExplosionPool { get => _explosionPool; set => _explosionPool = value; }

    //private AudioSource _audioSource;

    private void Awake()
    {
        //_audioSource = GetComponent<AudioSource>();
        //_audioSource.Play();
    }

    void Start()
    {
        
    }


    public void AnimationEnd()
    {
        _explosionPool.Release(this);       
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
