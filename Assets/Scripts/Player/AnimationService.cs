using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationService : MonoBehaviour
{
    [SerializeField] private  string _nameTransition;
    private Animator _animator;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayAnimationMotion(bool isPlay)
    {
        _animator.SetBool(_nameTransition, isPlay);
    }

}
