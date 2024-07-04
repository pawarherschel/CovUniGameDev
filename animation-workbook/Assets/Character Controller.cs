using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float jumpForce = 4f;
    [SerializeField] private float horizontalSpeed = 1.5f;
    [SerializeField] private float maxHorizontalSpeed = 3f;

    private Rigidbody2D _rigidbody2D;

    private Animator _animator;
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Jump = Animator.StringToHash("Jump");

    private bool _canJump = true;

    // Start is called before the first frame update
    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        _rigidbody2D.AddForce(Vector2.right * (Input.GetAxis("Horizontal") * horizontalSpeed));
        if (Input.GetKeyDown(KeyCode.Space) && _canJump)
        {
            _animator.SetTrigger(Jump);
            _rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            _canJump = false;
        }
        _animator.SetFloat(Speed, Mathf.Abs(_rigidbody2D.velocity.x));

        Vector3 scale = transform.localScale;

        scale.x = _rigidbody2D.velocity.x switch
        {
            > 0.05f => 1f,
            < 0f => -1f,
            _ => scale.x
        };

        transform.localScale = scale;

        _rigidbody2D.velocity =
            new Vector2(Mathf.Clamp(_rigidbody2D.velocity.x, -maxHorizontalSpeed, maxHorizontalSpeed),
                _rigidbody2D.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _canJump = true;
        _animator.ResetTrigger(Jump);
    }
}
