using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCBehavior : MonoBehaviour
{
    private bool isDistracted;
    public Transform RespawnPoint;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Distraction"))
        {
            isDistracted = true;
        }
        else if (other.CompareTag("Player") && isDistracted == false)
        {
            var life = FindAnyObjectByType<LevelUIController>();
            life.Kill();
            other.transform.position = RespawnPoint.position;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isDistracted = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
