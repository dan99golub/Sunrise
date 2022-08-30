using UnityEngine;
using Cinemachine;


public class CameraFollow : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;

    private Transform _target;

    public void Follow(Transform target)
    {
        _virtualCamera.Follow = target;
    }
}
