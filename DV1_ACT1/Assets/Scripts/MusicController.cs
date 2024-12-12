using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour
{
    [SerializeField] private float _maxMusicVolume = 1f;
    [SerializeField] private float _maxTimeForMaxVolume = 1f;
    [SerializeField] private float _curveSamplesNumber = 10f;
    [SerializeField] private AnimationCurve _musicVolumeCurve;

    private AudioSource _audioSource;

    private IEnumerator _musicVolumeCoroutine;

    

    


    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = 0.0f;
        //_audioSource.Play();

        _musicVolumeCoroutine = MusicVolumeCoroutine();
        StartCoroutine(_musicVolumeCoroutine);
    }

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    private IEnumerator MusicVolumeCoroutine()
    {
        float curveTime = 0;
        float timeBetweenIncrease = _maxTimeForMaxVolume/ _curveSamplesNumber;
        float curveIncrease = 1 / _curveSamplesNumber;

        //yield return new WaitForSeconds(1f);
        while (_audioSource.volume < _maxMusicVolume)
        {
            _audioSource.volume = _musicVolumeCurve.Evaluate(curveTime) * _maxMusicVolume;
            curveTime += curveIncrease;
            yield return new WaitForSeconds(timeBetweenIncrease);
        }

    }
}
