using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed;
    [SerializeField] private Vector2 _direction;
    [SerializeField] private Vector2 _nextDirection; 

    public Animator animator;

    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;
    [SerializeField] private float _rayLength;
    private LayerMask _obstaclesLayer;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 5F;
        _direction = Vector2.zero;
        _nextDirection = Vector2.zero;
        animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _rayLength = 0.05F;
        _obstaclesLayer = LayerMask.GetMask("Obstacles");

    }

    private RaycastHit2D boxCast(Vector2 direction) {
        Vector2 size = new Vector2(_spriteRenderer.bounds.size.x, _spriteRenderer.bounds.size.y);
        Vector2 origin = _rigidbody.position + size / 2;

        return Physics2D.BoxCast(origin, size, 0f, direction, _rayLength, _obstaclesLayer);
    }

    private bool isColliding(Vector2 direction) {
        RaycastHit2D hit = boxCast(direction);
        if (hit.collider != null && hit.collider.CompareTag("Obstacle")) return true;
        return false;
    }

    void OnDrawGizmos() {
        if (_rigidbody == null || _spriteRenderer == null) return;
        Vector2 size = new Vector2(_spriteRenderer.bounds.size.x, _spriteRenderer.bounds.size.y);
        Vector2 origin = _rigidbody.position + size / 2;
        Vector2 endPoint = origin + (_direction * _rayLength);

        RaycastHit2D hit = boxCast(_direction);
        if (isColliding(_direction))
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(origin + _direction * hit.distance, size);
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(endPoint, size);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {        
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
            _nextDirection = Vector2.up;
        }
        if (Input.GetKey(KeyCode.S ) || Input.GetKey(KeyCode.DownArrow)) {
            _nextDirection = Vector2.down;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            _nextDirection = Vector2.right;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            _nextDirection = Vector2.left;
        }

        if (!isColliding(_nextDirection)) _direction = _nextDirection;
        if (_direction == Vector2.up) {
            animator.SetBool("Up", true);
            animator.SetBool("Down", false);
            animator.SetBool("Left", false);
            animator.SetBool("Right", false);
        }
        if (_direction == Vector2.down) {
            animator.SetBool("Up", false);
            animator.SetBool("Down", true);
            animator.SetBool("Left", false);
            animator.SetBool("Right", false);
        }
        if (_direction == Vector2.left) {
            animator.SetBool("Up", false);
            animator.SetBool("Down", false);
            animator.SetBool("Left", true);
            animator.SetBool("Right", false);
        }
        if (_direction == Vector2.right) {
            animator.SetBool("Up", false);
            animator.SetBool("Down", false);
            animator.SetBool("Left", false);
            animator.SetBool("Right", true);
        }
        if (!isColliding(_direction)) transform.Translate(_direction * moveSpeed * Time.deltaTime); // (0, 1)
    }
}
