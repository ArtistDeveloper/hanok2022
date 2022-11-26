using UnityEngine;

public class CheckEnterTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            var wall = collision.GetComponent<Wall>();
            wall?.ValidateWallShadow();
        }
    }
}