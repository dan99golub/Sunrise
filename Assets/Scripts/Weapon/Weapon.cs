using UnityEngine;
using Mirror;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform _spawnTransform;
    [SerializeField] private float _coolDown;
    [SerializeField] private float _distantionAttack;

    private float _lastAttackTime = 0;

    public float DistantionAttack => _distantionAttack;

    public void Shoot()
    {
        if (_lastAttackTime >= _coolDown)
        {
            var newBullet = Instantiate(_bulletPrefab, _spawnTransform.position, _spawnTransform.rotation);
            NetworkServer.Spawn(newBullet.gameObject);
            newBullet.Move(_spawnTransform.up);
        }
    }

    private void Update()
    {

        if (_lastAttackTime >= _coolDown)
        {
            _lastAttackTime = 0;
        }

        _lastAttackTime += Time.deltaTime;
    }
}
