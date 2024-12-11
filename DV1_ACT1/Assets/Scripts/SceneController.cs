using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    static public float MinX { get; private set; }
    static public float MaxX { get; private set; }
    static public float MinY { get; private set; }
    static public float MaxY { get; private set; }


    [SerializeField] private Camera _mainCamera;


    private float _cameraSize;
    private float _aspectRatio;

    void Start()
    {
        _mainCamera.orthographic = true;
        _cameraSize = _mainCamera.orthographicSize;
        _aspectRatio = _mainCamera.aspect;

        MinX = _cameraSize * _aspectRatio * -1;
        MaxX = _cameraSize * _aspectRatio;
        MinY = _cameraSize * -1;
        MaxY = _cameraSize;
    }


    void Update()
    {
        
    }
}
