using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _direction;

    private SpriteRenderer _spriteRenderer;
    private float _spriteLenght;
    private Vector3 _initPosition;

    void Start()
    {
        _spriteLenght = GetComponent<SpriteRenderer>().size.x;
        _spriteLenght *= transform.localScale.x;
        _initPosition = transform.position;
    }

    
    void Update()
    {
        float resto = (_speed * Time.time) % _spriteLenght;
        transform.position = _initPosition + resto * _direction;
    }
}
