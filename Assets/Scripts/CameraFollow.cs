using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(
            transform.position,
            new Vector3(target.position.x, 
                transform.position.y, 
                transform.position.z),
            10 * Time.deltaTime);


    }
}
