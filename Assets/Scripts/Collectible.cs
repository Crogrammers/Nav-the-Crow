using UnityEngine;

public class Collectible : MonoBehaviour
{
    // On trigger enter
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FindAnyObjectByType<LevelUIController>().collect();
            Destroy(gameObject);
        }
    }
}
