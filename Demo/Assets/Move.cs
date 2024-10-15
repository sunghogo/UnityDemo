using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Direction {
    Up,
    Down,
    Left,
    Right
}

public class Move : MonoBehaviour
{

    public float moveSpeed = 5f;
    public Animator animator;
    public Direction playerDirection;

    void Start() {
        animator = GetComponent<Animator>();
        playerDirection = Direction.Right;
    }
    void Update()
    {
        Vector2 moveVector = Vector2.zero;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            playerDirection = Direction.Up;
            moveVector += Vector2.up;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            playerDirection = Direction.Down;
            moveVector += Vector2.down;
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            playerDirection = Direction.Left;            
            moveVector += Vector2.left;
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            playerDirection = Direction.Right;            
            moveVector += Vector2.right;

        }

        if (moveVector == Vector2.zero) {
            animator.enabled = false;
        } else {
            animator.enabled = true;
        }

        // Normalize the movement direction to maintain consistent speed when moving diagonally
        moveVector.Normalize();

        animator.SetInteger("Direction", (int)playerDirection);
        // Move the player based on the moveDirection and moveSpeed
        transform.Translate(moveVector * moveSpeed * Time.deltaTime, 0);
    }
}
