using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour {
    [Header("References")]
    [SerializeField] private InputReader input;

    [Header("attributes")]
    [SerializeField] private float speed = 10f;

    private Vector2 _direction;
    private bool _isJumping;

    private void Start() {
        // Debug.Log(input.MoveEvent);
        input.MoveEvent += HandleMove;
        input.JumpEvent += HandleJump;
        input.JumpCancelledEvent += HandleJumpCancelled;
    }

    private void HandleMove(Vector2 direction) {
        _direction = direction;
    }

    private void HandleJump() {
        _isJumping = true;
    }

    private void HandleJumpCancelled() {
        _isJumping = false;
    }

    private void Move() {
        if (_direction == Vector2.zero) {
            return;
        }

        transform.Translate(_direction * (speed * Time.deltaTime));
    }

    private void Jump() {
        if (_isJumping) {
            Debug.Log("Jumping");
        }
    }

    void Update() {
        Move();
        Jump();
        
        // if (Input.GetKey(KeyCode.W)) {
        //     transform.Translate(0, speed * Time.deltaTime, 0);
        // }
        //
        // if (Input.GetKey(KeyCode.S)) {
        //     transform.Translate(0, -1 * speed * Time.deltaTime, 0);
        // }
        //
        // if (Input.GetKey(KeyCode.A)) {
        //     transform.Translate(-1 * speed * Time.deltaTime, 0, 0);
        // }
        //
        // if (Input.GetKey(KeyCode.D)) {
        //     transform.Translate(speed * Time.deltaTime, 0, 0);
        // }
    }
}