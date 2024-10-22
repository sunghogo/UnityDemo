using UnityEngine;


public enum Direction {
    Up,
    Down,
    Left,
    Right
}

public class Move : MonoBehaviour
{
    public float moveSpeed = 2f;
    public Vector2 moveVector = Vector2.zero;
    public Animator animator;
    public Direction playerDirection;
    private Rigidbody2D _rigidbody;
    private LayerMask _obstaclesLayer;  
    private SpriteRenderer _spriteRenderer;
    public float rayLength = 0.5f;


    private bool IsBlocked(Vector2 direction)
    {
        Vector2 vector1 = _rigidbody.position;
        vector1.x += _spriteRenderer.bounds.size.x / 8;
        vector1.y += _spriteRenderer.bounds.size.y / 8;
        switch (playerDirection) {
            case Direction.Up:
                vector1.y += _spriteRenderer.bounds.size.y * 3 / 4;
                break;
            case Direction.Right:
                vector1.x += _spriteRenderer.bounds.size.x * 3 / 4;
                break;
        }

        Vector2 vector2 = vector1;
        switch(playerDirection) {
            case Direction.Up:
            case Direction.Down:
                vector2.x += _spriteRenderer.bounds.size.x * 3 / 4;
                break;
            case Direction.Left:
            case Direction.Right:
                vector2.y += _spriteRenderer.bounds.size.y * 3 / 4;
                break;
        }

        Debug.DrawRay(vector1, moveVector * 2f, Color.green);
        Debug.DrawRay(vector2, moveVector * 2f, Color.green);
        Debug.DrawRay(vector1, moveVector * rayLength, Color.red);
        Debug.DrawRay(vector2, moveVector * rayLength, Color.red);
        RaycastHit2D hit1 = Physics2D.Raycast(vector1, direction, rayLength, _obstaclesLayer);
        RaycastHit2D hit2 = Physics2D.Raycast(vector2, direction, rayLength, _obstaclesLayer);
        if (hit1.collider != null) {
            Debug.Log("Hit an object with the Wall tag: " + hit1.collider.name);
            return true; // Blocked by wall
        } else if (hit2.collider != null) {
            Debug.Log("Hit an object with the Wall tag: " + hit2.collider.name);
            return true; // Blocked by wall
        }

        return false; // No wall in the way
    }

    void Start() {
        animator = GetComponent<Animator>();
        playerDirection = Direction.Right;
        _rigidbody = GetComponent<Rigidbody2D>();
        _obstaclesLayer = LayerMask.GetMask("Obstacles");
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    void Update()
    {
        if (GameManager.Instance.isStarted) {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) )
            {
                playerDirection = Direction.Up;
                moveVector += Vector2.up;
            }
            else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                playerDirection = Direction.Down;
                moveVector += Vector2.down;
            }
            else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                playerDirection = Direction.Left;            
                moveVector += Vector2.left;
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                playerDirection = Direction.Right;            
                moveVector += Vector2.right;
            }
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
        if(!IsBlocked(moveVector)) transform.Translate(moveVector * moveSpeed * Time.deltaTime, 0);
        else moveVector = Vector2.zero;
    }
}
