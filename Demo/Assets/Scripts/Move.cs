using System.Data.Common;
using UnityEngine;
using UnityEngine.UIElements;

public enum Direction {
    Up,
    Down,
    Left,
    Right
}

public class Move : MonoBehaviour
{
    public float moveSpeed;
    public Vector2 moveVector;
    public Vector2 nextMoveVector;
    public Direction playerDirection;
    public Direction nextPlayerDirection;
    public Animator animator;
    private Rigidbody2D _rigidbody;
    private LayerMask _obstaclesLayer;  
    private SpriteRenderer _spriteRenderer;
    public float rayLength;
    bool isMirroring;
    
    // Box casts a box the size of sprite, shifted rayLength units towards the front
    private RaycastHit2D boxCast(Vector2 direction) {
        Vector2 size = new Vector2(_spriteRenderer.bounds.size.x, _spriteRenderer.bounds.size.y);
        Vector2 origin = _rigidbody.position + size / 2;

        return Physics2D.BoxCast(origin, size, 0f, direction, rayLength, _obstaclesLayer);
    }
    
    // Checks if there is obstacle rayLength ahead of given direction vector
    private bool isBlocked(Vector2 direction)
    {
        RaycastHit2D hit = boxCast(direction);
        if (hit.collider != null && hit.collider.CompareTag("Obstacle")) {
            return true;  
        }
        return false;
    }

    void Start() {
        moveSpeed = 5f;
        moveVector = nextMoveVector = Vector2.zero;
        playerDirection = nextPlayerDirection = Direction.Right;
        rayLength = 0.05f;
        animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _obstaclesLayer = LayerMask.GetMask("Obstacles");
        _spriteRenderer = GetComponent<SpriteRenderer>();
        isMirroring = false;
    }

    void OnDrawGizmos() {
        if (_rigidbody == null || _spriteRenderer == null) return;
        Vector2 size = new Vector2(_spriteRenderer.bounds.size.x, _spriteRenderer.bounds.size.y);
        Vector2 origin = _rigidbody.position + size / 2;
        Vector2 direction = nextMoveVector;
        Vector2 endPoint = origin + (direction * rayLength);

        RaycastHit2D hit = boxCast(direction);
        if (hit.collider != null && hit.collider.CompareTag("Obstacle"))
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(origin + direction * hit.distance, size);
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(endPoint, size);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Mirror") {
            if (!isMirroring) transform.position = new Vector3(-transform.position.x, transform.position.y, transform.position.z);
            isMirroring = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Mirror") {
            isMirroring = false;
        }
    }
    
    void FixedUpdate()
    {
        if (GameManager.Instance.isStarted) {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) )
            {
                nextPlayerDirection = Direction.Up;
                nextMoveVector = Vector2.up;
            }
            else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                nextPlayerDirection = Direction.Down;
                nextMoveVector = Vector2.down;
            }
            else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                nextPlayerDirection = Direction.Left;            
                nextMoveVector = Vector2.left;
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                nextPlayerDirection = Direction.Right;            
                nextMoveVector = Vector2.right;
            }
        }

        if (moveVector == Vector2.zero) {
            animator.enabled = false;
        } else {
            animator.enabled = true;
        }

        // Move the player based on the moveDirection and moveSpeed
        if(!isBlocked(nextMoveVector)) {
            moveVector = nextMoveVector;
            playerDirection = nextPlayerDirection;
        }
        if(!isBlocked(moveVector)) transform.Translate(moveVector * moveSpeed * Time.deltaTime, 0);
        else moveVector = Vector2.zero;

        animator.SetInteger("Direction", (int)playerDirection);
    }
}
