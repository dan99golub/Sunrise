using Mirror;
using UnityEngine;

public class SpawnerZombie : MonoBehaviour
{
    [SerializeField] private Zombie[] _enemiesPrefab;
    [SerializeField] private int _number;
    [SerializeField] private float _spawnRadius;
    [SerializeField] private ContainerZombie _containerZombie;

    private Transform[] _spawnPositions;
    private Collider[] _collides;

    void Start()
    {
        _spawnPositions = GetComponentsInChildren<Transform>();
     
        Spawn(_number);
    }

    private void Spawn(int count = 10)
    {

        for (int i = 0; i < count; i++)
        {
            Vector3 _randomPosition = GetPosition();
            var enemyPrefab = _enemiesPrefab[Random.Range(0, _enemiesPrefab.Length)];
            var newEnemy = Instantiate(enemyPrefab, _randomPosition, Quaternion.identity);
            NetworkServer.Spawn(newEnemy.gameObject);
            _containerZombie.AddZombie(newEnemy);
        }
    }

    private Vector3 GetPosition()
    {
        var randomCircle = Random.insideUnitSphere * _spawnRadius;
        var spawnPoint = _spawnPositions[Random.Range(0, _spawnPositions.Length)];
        var point = new Vector3(spawnPoint.position.x + randomCircle.x, spawnPoint.position.y, spawnPoint.position.z + randomCircle.z);
        var checkPosition = CheckPoint(point);
        if (checkPosition)
        {
            return point;
        }
        else
        {
            return GetPosition();

        }

    }

    private bool CheckPoint(Vector3 spawnPosition)
    {
        _collides = Physics.OverlapBox(spawnPosition, new Vector3(0.5f, 0, 0.5f));

        if (_collides.Length > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
