using UnityEngine;
using Mirror;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : NetworkBehaviour
{
    [SyncVar] [SerializeField] private float _speed;
    [SyncVar] [SerializeField] private float _damage;

    private Rigidbody2D _rigidBody;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector3 direction)
    {
        _rigidBody.AddForce(direction * _speed, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Health health))
        {
            Debug.Log("dAMAGE");
            health.TakeDamage(_damage);
            NetworkServer.Destroy(gameObject);
            Destroy(gameObject);
        }

        Destroy(gameObject, 1.5f);
    }
}