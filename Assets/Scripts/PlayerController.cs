using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float speed;
    [SerializeField, Min(0)] private float shotCooldownTime = 0.1f;
    public Rigidbody Rigidbody => _rb;
    private Vector2 input;
    private void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
    }
    private void FixedUpdate()
    {
        MovementFixedUpdate(input);
    }

    private void MovementFixedUpdate(Vector2 input)
    {
        var movement = (new Vector3(input.x, 0, input.y).normalized * Time.deltaTime * speed) + _rb.position;
        _rb.MovePosition(movement);
    }
}
