using UnityEngine;

public class PowerBullet : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Pacman")) Destroy(gameObject);
    }
}
