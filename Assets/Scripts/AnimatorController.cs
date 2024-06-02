using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void UpdateAnimations(Vector2 input)
    {
        input.Normalize();
        _animator.SetFloat("movementX", input.x);
        _animator.SetFloat("movementY", input.y);
    }
}