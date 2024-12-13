using UnityEngine;
using UnityEngine.Pool;

public class Explosion : MonoBehaviour
{

    private ObjectPool<Explosion> _explosionPool;
    public ObjectPool<Explosion> ExplosionPool { get => _explosionPool; set => _explosionPool = value; }


    public void AnimationEnd()
    {
        _explosionPool.Release(this);       
    }


}
