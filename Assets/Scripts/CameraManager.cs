using NUnit.Framework;
using System;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Camera playerCam, buildCam;
    [SerializeField] private Collider2D camArea;
    [SerializeField] private float maxCamSize, minCamSize, zoomSpeed, moveSpeed;
    private Bounds bounds, camBounds;

    //float height = camera.orthographicSize;
    //float width = height + camera.aspect;

    void Start()
    {
        bounds = camArea.bounds;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerCam.isActiveAndEnabled)
            CamFollow();
    }

    private void Update()
    {
        if (buildCam.isActiveAndEnabled)
            CamMove();
    }

    private void CamMove()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Camera.main.orthographicSize -= scroll * zoomSpeed;
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minCamSize, maxCamSize);

        float height = Camera.main.orthographicSize;
        float width = height * Camera.main.aspect;

        float minX = bounds.min.x + width;
        float maxX = bounds.max.x - width;
        float minY = bounds.min.y + height;
        float maxY = bounds.max.y - height;

        camBounds = new Bounds();
        camBounds.SetMinMax(new Vector3(minX, minY, 0f), new Vector3(maxX, maxY, 0f));

        float moveX = Input.GetAxis("Horizontal") / moveSpeed;
        float moveY = Input.GetAxis("Vertical") / moveSpeed;

        Vector3 targetPos = new Vector3(Camera.main.transform.position.x + moveX, Camera.main.transform.position.y + moveY, Camera.main.transform.position.z);
        targetPos = new Vector3(Mathf.Clamp(targetPos.x, camBounds.min.x, camBounds.max.x), Mathf.Clamp(targetPos.y, camBounds.min.y, camBounds.max.y), targetPos.z);
        Camera.main.transform.position = targetPos;
    }

    private void CamFollow()
    {
        Transform target = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 targetPos = new Vector3(target.position.x, target.position.y, Camera.main.transform.position.z);

        float height = Camera.main.orthographicSize;
        float width = height * Camera.main.aspect;

        float minX = bounds.min.x + width;
        float maxX = bounds.max.x - width;
        float minY = bounds.min.y + height;
        float maxY = bounds.max.y - height;

        camBounds = new Bounds();
        camBounds.SetMinMax(new Vector3(minX, minY, 0f), new Vector3(maxX, maxY, 0f));

        targetPos = new Vector3(Mathf.Clamp(targetPos.x, camBounds.min.x, camBounds.max.x), Mathf.Clamp(targetPos.y, camBounds.min.y, camBounds.max.y), targetPos.z);

        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, targetPos, 10 * Time.deltaTime);
    }

    public void SwitchCamera()
    {
        if (playerCam.isActiveAndEnabled)
        {
            buildCam.gameObject.SetActive(true);
            playerCam.gameObject.SetActive(false);
        }
        else if (buildCam.isActiveAndEnabled)
        {
            playerCam.gameObject.SetActive(true);
            buildCam.gameObject.SetActive(false);
        }
    }
}
