using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private bool GameOver = false;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.anyKeyDown && !GameOver)
        {
            _rigidbody2D.velocity = Vector2.up * 45f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Game over char");
        GameOver = true;
    }
}
