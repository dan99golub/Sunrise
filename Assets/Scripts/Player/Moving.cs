using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Vector2 _direction;

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(_direction * _speed * Time.fixedDeltaTime);
    }

    public void SetDirection(Vector2 direction)
    {
        _direction = direction; 
    }
}
