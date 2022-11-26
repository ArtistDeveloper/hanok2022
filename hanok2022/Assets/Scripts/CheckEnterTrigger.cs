using UnityEngine;

public class CheckEnterTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.LogError("enter someing");
        
        if (collision.CompareTag("Wall"))
        {
            var wall = collision.GetComponent<Wall>();

            Debug.LogError("enter wall");
            Destroy(wall.gameObject);

            // todo : check wall data
        }
    }
}
