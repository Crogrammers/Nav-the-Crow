using UnityEngine;

public class Collectible : MonoBehaviour
{
    // On trigger enter
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<LevelUIController>().collect();
            Destroy(gameObject);
        }
    }
}
