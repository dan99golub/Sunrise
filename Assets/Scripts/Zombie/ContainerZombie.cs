using System.Collections.Generic;
using UnityEngine;

public class ContainerZombie : MonoBehaviour
{
    private List<Zombie> _zombies = new List<Zombie>();
    private List<Player> _targets = new List<Player>();

    public void AddZombie(Zombie zombie)
    {
        _zombies.Add(zombie);
    }

    private void Start()
    {
        ContainerPlayers.Instance.AddedPlayer += SetTarget;
    }

    private void OnDisable()
    {
        ContainerPlayers.Instance.AddedPlayer -= SetTarget;
    }

    private void AddTarget(Player target)
    {
        _targets.Add(target);
    }


    private void SetTarget(Player target)
    {
        AddTarget(target);
        int countEnemies = GetCountZombiesForTarget();
        if (countEnemies == _zombies.Count)
        {
            _zombies.ForEach(zombie => zombie.SetTarget(target));
        }
        else
        {
            for (int i = 0; i < countEnemies; i++)
            {
                _zombies[Random.Range(0, _zombies.Count)].SetTarget(target);
            }
        }
    }

    private int GetCountZombiesForTarget()
    {
        if (_targets.Count == 1)
        {
            return _zombies.Count;
        }

        return _zombies.Count / _targets.Count;
    }
}