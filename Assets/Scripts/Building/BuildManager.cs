using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    private Transform currentObj;
    [SerializeField] private GameObjListSO grabableObjs, currentObjSO;

    public void Spawn(GameObject spawn)
    {
        if (currentObj != null)
            return;
        
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentObj = Instantiate(spawn, new Vector2(mousePos.x, mousePos.y), Quaternion.identity).transform;
        currentObj.GetChild(0).gameObject.SetActive(true);
    }

    void Start()
    {
        grabableObjs.objsList.Clear();
        //currentObjSO.objsList.Clear();
    }

    void Update()
    {
        if (currentObj != null)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //currentObj.position = Vector3.Lerp(currentObj.position, new Vector3(mousePos.x, mousePos.y, transform.position.z), 10 * Time.deltaTime);
            currentObj.position = Vector3.Lerp(currentObj.position, new Vector3(RoundToGrid(mousePos).x, RoundToGrid(mousePos).y, transform.position.z), 10 * Time.deltaTime);
            currentObj.GetChild(0).gameObject.SetActive(true);

            if (Input.GetMouseButtonDown(0))
            {
                currentObj.position = new Vector3(RoundToGrid(mousePos).x, RoundToGrid(mousePos).y, transform.position.z);
                currentObj.GetChild(0).gameObject.SetActive(false);
                currentObj = null;
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0) && grabableObjs.objsList.Count != 0)
                currentObj = grabableObjs.objsList[0];

            if (grabableObjs.objsList.Count != 0)
                grabableObjs.objsList[0].GetChild(0).gameObject.SetActive(true);
        }

        currentObjSO.objsList[0] = currentObj;
    }

    Vector3 RoundToGrid(Vector3 mousePos)
    {
        Vector3 rounded = mousePos;

        float x = Mathf.Round(rounded.x);
        float y = Mathf.Round(rounded.y);

        rounded = new Vector3(x, y, rounded.z);

        return rounded;
    }
}
