using Unity.VisualScripting;
using UnityEngine;

public class GlassBreak : MonoBehaviour
{
    [SerializeField] private float timeTillBreak = 3;

    [SerializeField] private float breakTimer;

    bool onGlass = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (onGlass)
        {
            breakTimer += Time.deltaTime;
            //Debug.Log(breakTimer);
        }
        //breakTimer = Time.deltaTime;
        if (breakTimer >= timeTillBreak)
        {
            Destroy(this.gameObject);
        }
        

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            onGlass = true;
            Debug.Log("are you sure");
        }
       
        //breakTimer = Time.deltaTime;

        //else if (other.gameObject.CompareTag("Build Material"))
        //;
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            onGlass = false;
            Debug.Log("are you not sure");
        }
    }

}
