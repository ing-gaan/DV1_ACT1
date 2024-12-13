using UnityEngine;
using UnityEngine.Pool;

public class ExplosionSpawner : MonoBehaviour
{
    [Header("---------- Explosion prefab")]
    [SerializeField] private Explosion _explosion;

    
    private ObjectPool<Explosion> _explosionPool;
    private Vector3 _spawnPosition;


    private void Awake()
    {
        _explosionPool = new ObjectPool<Explosion>(CreateExplosion, GetExplosion, ReleaseExplosion, DestroyExplosion);       
    }


    public void SpawnExplosion(Vector3 spawnPosition)
    {
        _spawnPosition = spawnPosition;
        _explosionPool.Get();
    }


    private Explosion CreateExplosion()
    {
        Explosion explosion = Instantiate(_explosion, _spawnPosition, Quaternion.identity);
        explosion.ExplosionPool = _explosionPool;
        return explosion;
    }
    private void GetExplosion(Explosion explosion)
    {
        explosion.transform.position = _spawnPosition;
        explosion.gameObject.SetActive(true);
    }
    private void ReleaseExplosion(Explosion explosion)
    {
        explosion.gameObject.SetActive(false);
    }
    private void DestroyExplosion(Explosion explosion)
    {
        Destroy(explosion.gameObject);
    }

}
