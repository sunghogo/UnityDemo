using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public float moveSpeed;
    private Vector2 _direction;
    public Animator animator;

    public Vector2 futureDirection;

    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;
    [SerializeField] private float _rayLength;
    private LayerMask _obstaclesLayer;

    public bool canTeleport;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 5F;
        _direction = Vector2.zero;
        animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _rayLength = 0.05F;
        futureDirection = Vector2.zero;
        _obstaclesLayer = LayerMask.GetMask("Obstacles");
        canTeleport = true;

    }

    private RaycastHit2D boxCast(Vector2 _direction)
    {
        Vector2 size = new Vector2(_spriteRenderer.bounds.size.x, _spriteRenderer.bounds.size.y);
        Vector2 origin = _rigidbody.position + size / 2; // if pacman is offset

        return Physics2D.BoxCast(origin, size, 0f, _direction, _rayLength, _obstaclesLayer);
    }

    private bool isColliding(Vector2 _direction)
    {
        RaycastHit2D hit = boxCast(_direction);
        if (hit.collider != null && hit.collider.CompareTag("Obstacle")) return true;
        return false;
    }


    void OnDrawGizmos()
    {
        if (_rigidbody == null || _spriteRenderer == null) return;
        Vector2 size = new Vector2(_spriteRenderer.bounds.size.x, _spriteRenderer.bounds.size.y);
        Vector2 origin = _rigidbody.position + size / 2; // if pacman is offset
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


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Mirror"))
        {
            if (canTeleport) transform.position = new Vector3(-transform.position.x, transform.position.y, transform.position.z);
            canTeleport = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Mirror")) canTeleport = true;
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            futureDirection = Vector2.up;

        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            futureDirection = Vector2.down;

        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            futureDirection = Vector2.right;

        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            futureDirection = Vector2.left;

        }

        if (!isColliding(futureDirection)) _direction = futureDirection;

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

        if (!isColliding(_direction)) transform.Translate(_direction * moveSpeed * Time.deltaTime);

    }
}
