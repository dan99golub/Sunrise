using System;
using UnityEngine;
using Mirror;

public class Health : NetworkBehaviour
{
    [SyncVar] [SerializeField] private float _count;

    [SyncVar] protected float CurrentHealth;

    public Action Die;

    private void Start()
    {
        CurrentHealth = _count;
    }

    [Server]
    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            Die?.Invoke();
            Destroy();
        }
    }

    [Server]
    private void Destroy()
    {
        NetworkServer.Destroy(gameObject);
        Destroy(gameObject);
    }

    [Command]
    public void CmdTakeDamage(float damage)
    {
        TakeDamage(damage); 
    }
}
