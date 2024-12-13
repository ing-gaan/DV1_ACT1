using UnityEngine;

public class Parallax : MonoBehaviour
{
    [Header("---------- Parallax adjusments")]
    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _direction;

    private SpriteRenderer _spriteRenderer;
    private float _spriteLenght;
    private Vector3 _initPosition;

    private void Start()
    {
        _spriteLenght = GetComponent<SpriteRenderer>().size.x;
        _spriteLenght *= transform.localScale.x;
        _initPosition = transform.position;
    }

    
    private void Update()
    {
        float resto = (_speed * Time.time) % _spriteLenght;
        transform.position = _initPosition + resto * _direction;
    }
}
