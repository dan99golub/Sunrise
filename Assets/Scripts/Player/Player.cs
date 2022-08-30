using Mirror;
using UnityEngine;

[RequireComponent(typeof(HeroMove))]
public class Player : NetworkBehaviour
{
    [SerializeField] private Weapon[] _weapons;
    [SerializeField] private Weapon _currentWeapon;
    [SerializeField] private LayerMask _layer;

    private HeroMove _heroMove;

    private void Awake()
    {
        _heroMove = GetComponent<HeroMove>();
    }

    [Server]
    private void CmdAttack()
    {
        var positionEnemy = FindEnemy();

        if (positionEnemy == Vector3.zero)
            return;

        var direction = positionEnemy - transform.position;
        _heroMove.RequestToTurn(direction);
        _currentWeapon.Shoot();
    }

    public override void OnStartClient()
    {
        ContainerPlayers.Instance.AddPlayer(this);
    }

    private Vector3 FindEnemy()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, _currentWeapon.DistantionAttack, _layer);
     
        if (collider!= null)
        {
            return collider.transform.position;  
        }
        return Vector2.zero;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            CmdAttack();
        }
    }
}
