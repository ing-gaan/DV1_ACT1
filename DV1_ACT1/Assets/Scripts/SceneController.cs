using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    static public float MinX { get; private set; }
    static public float MaxX { get; private set; }
    static public float MinY { get; private set; }
    static public float MaxY { get; private set; }


    [SerializeField] private GameObject _boundaryPrefab;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private float _boundaryWidth = 1.0f;
    [SerializeField] private float _shiftBoundaryAdjust = 2.0f;


    private float _cameraSize;
    private float _aspectRatio;

    void Awake()
    {
        _mainCamera.orthographic = true;
        _cameraSize = _mainCamera.orthographicSize;
        _aspectRatio = _mainCamera.aspect;

        MinX = _cameraSize * _aspectRatio * -1;
        MaxX = _cameraSize * _aspectRatio;
        MinY = _cameraSize * -1;
        MaxY = _cameraSize;
    }


    private void Start()
    {
        CreateBoundaries();
    }


    void Update()
    {
        
    }

    private void CreateBoundaries()
    {
        GameObject boundaryObj;

        boundaryObj = Instantiate(_boundaryPrefab, Vector3.zero, Quaternion.identity);
        SetBoundarieSize(boundaryObj, Vector3.up);
        boundaryObj = Instantiate(_boundaryPrefab, Vector3.zero, Quaternion.identity);
        SetBoundarieSize(boundaryObj, Vector3.right);
        boundaryObj = Instantiate(_boundaryPrefab, Vector3.zero, Quaternion.identity);
        SetBoundarieSize(boundaryObj, Vector3.left);
        boundaryObj = Instantiate(_boundaryPrefab, Vector3.zero, Quaternion.identity);
        SetBoundarieSize(boundaryObj, Vector3.down);
    }


    private void SetBoundarieSize(GameObject boundaryObj, Vector3 boundaryLocation)
    {
        float shift;
        boundaryObj.GetComponent<BoxCollider2D>().size = new Vector2(_cameraSize * _aspectRatio * 2, _boundaryWidth);

        bool isOnOneSide = boundaryLocation == Vector3.left || boundaryLocation == Vector3.right;
        if (isOnOneSide)
        {
            boundaryObj.transform.eulerAngles = new Vector3(0, 0, -90);
            shift = MaxX + _shiftBoundaryAdjust;
        }
        else
        {
            shift = MaxY + _shiftBoundaryAdjust;
        }
        boundaryObj.transform.position = boundaryLocation * shift;
    }




}
