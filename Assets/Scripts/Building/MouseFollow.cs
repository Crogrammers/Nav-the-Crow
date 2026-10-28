using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    [SerializeField] private GameObject follower;
    [SerializeField] private GameObjListSO grabableObjs, currentObjSO;

    void Update()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        follower.transform.position = new Vector2(worldPoint.x, worldPoint.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Build Material"))
            return;

        grabableObjs.objsList.Add(collision.transform);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Build Material"))
            return;

        if (currentObjSO.objsList[0] == null)
            collision.transform.GetChild(0).gameObject.SetActive(false);

        grabableObjs.objsList.Remove(collision.transform);
    }
}
