using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _speed = 5.0f;

    private float _halfSpriteX;
    private float _halfSpriteY;
    private float _clampMinX;
    private float _clampMaxX;
    private float _clampMinY;
    private float _clampMaxY;


    void Start()
    {
        SetDefaults();
    }

    private void SetDefaults()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        _halfSpriteX = spriteRenderer.size.x / 2;
        _halfSpriteY = spriteRenderer.size.y / 2;
        _clampMinX = SceneController.MinX + _halfSpriteX;
        _clampMaxX = SceneController.MaxX - _halfSpriteX;
        _clampMinY = SceneController.MinY + _halfSpriteY;
        _clampMaxY = SceneController.MaxY - _halfSpriteY;
    }

    void Update()
    {
        float inputH = Input.GetAxisRaw("Horizontal");
        float inputV = Input.GetAxisRaw("Vertical");

        transform.Translate(new Vector3(inputH, inputV, 0).normalized * _speed * Time.deltaTime);

        float clampX = Mathf.Clamp(transform.position.x, _clampMinX, _clampMaxX);
        float clampY = Mathf.Clamp(transform.position.y, _clampMinY, _clampMaxY);
        transform.position = new Vector3(clampX, clampY, transform.position.z);
    }
}
