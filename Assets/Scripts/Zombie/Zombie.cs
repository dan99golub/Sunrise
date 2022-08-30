using UnityEngine;
using Mirror;

public class Zombie : NetworkBehaviour
{
    [SyncVar] [SerializeField] private float _speed;
    [SerializeField] private float _angleModifier;
    private Player _target;

    public void SetTarget(Player target)
    {
        _target = target;
    }

    private void Update()
    {
        if (_target != null)
        {
            var direction = Vector2.MoveTowards(transform.position, _target.transform.position, _speed*Time.deltaTime);
            LookAt(direction, _angleModifier);
            Move(direction);
        }
    }

    private void Move(Vector2 direction)
    {
        transform.position = direction;
    }

    public  void LookAt(Vector2 targetPos, float angleModifier)
    {
        Vector2 lookDir = targetPos - new Vector2(transform.position.x, transform.position.y);

        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        transform.localEulerAngles = new Vector3(0, 0, angle + angleModifier);
    }
}
