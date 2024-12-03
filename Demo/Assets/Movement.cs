using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 5F;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
            animator.SetBool("Up", true);
            animator.SetBool("Down", false);
            animator.SetBool("Left", false);
            animator.SetBool("Right", false);
            transform.Translate(Vector2.up * moveSpeed * Time.deltaTime); // (0, 1)
        }
        if (Input.GetKey(KeyCode.S ) || Input.GetKey(KeyCode.DownArrow)) {
            animator.SetBool("Up", false);
            animator.SetBool("Down", true);
            animator.SetBool("Left", false);
            animator.SetBool("Right", false);
            transform.Translate(Vector2.down * moveSpeed * Time.deltaTime); // (0, 1)
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            animator.SetBool("Up", false);
            animator.SetBool("Down", false);
            animator.SetBool("Left", false);
            animator.SetBool("Right", true);
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime); // (0, 1)
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            animator.SetBool("Up", false);
            animator.SetBool("Down", false);
            animator.SetBool("Left", true);
            animator.SetBool("Right", false);
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime); // (0, 1)
        }
    }
}
