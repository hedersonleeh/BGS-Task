using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float speed;
    [SerializeField, Min(0)] private float shotCooldownTime = 0.1f;
    AnimatorController _animatorController;
    public Rigidbody2D Rigidbody => _rb;
    private Vector2 input;
    private void Awake()
    {
        _animatorController = GetComponent<AnimatorController>();
    }
    private void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        _animatorController.UpdateAnimations(input);
    }
    private void FixedUpdate()
    {
        MovementFixedUpdate(input);
    }

    private void MovementFixedUpdate(Vector2 input)
    {
        var movement = (new Vector2(input.x, input.y).normalized * Time.deltaTime * speed) + (_rb.position);
        _rb.MovePosition(movement);
    }
}
