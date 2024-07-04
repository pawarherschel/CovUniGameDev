using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBack : MonoBehaviour
{
    [SerializeField] private float speed;

    private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();

        _rigidbody2D.velocity = Vector2.left * speed;
    }
}
