using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class BuildManager : MonoBehaviour
{
    private Transform currentObj;
    [SerializeField] float gridX, gridY;
    [SerializeField] GameObject buildUI;
    [SerializeField] BoolSO buildMode;
    [SerializeField] private GameObjListSO grabableObjs, currentObjSO;
    [SerializeField] private TextMeshProUGUI costTEMPTHINGtext;
    private int costTEMPTHINGGint = 0;

    public void Spawn(GameObject spawn)
    {
        if (currentObj != null)
            return;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentObj = Instantiate(spawn, new Vector2(mousePos.x, mousePos.y), Quaternion.identity).transform;
        currentObj.GetChild(0).gameObject.SetActive(true);

        costTEMPTHINGGint += 1;
    }

    public void SetBuildMode(bool boolean)
    {
        buildMode.boolean = boolean;
    }

    void Start()
    {
        grabableObjs.objsList.Clear();
        //currentObjSO.objsList.Clear();
        buildMode.boolean = true;
    }

    void Update()
    {
        if (buildMode.boolean)
        {
            buildUI.SetActive(true);
            BuildAndMove();
        }
        else
        {
            buildUI.SetActive(false);
        }

        costTEMPTHINGtext.text = $"Cost: {costTEMPTHINGGint}";
    }

    void BuildAndMove()
    {
        if (currentObj != null)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //currentObj.position = Vector3.Lerp(currentObj.position, new Vector3(mousePos.x, mousePos.y, transform.position.z), 10 * Time.deltaTime);
            currentObj.position = Vector3.Lerp(currentObj.position, new Vector3(RoundToGrid(mousePos).x, RoundToGrid(mousePos).y, transform.position.z), 10 * Time.deltaTime);
            currentObj.GetChild(0).gameObject.SetActive(true);

            if (Input.GetMouseButtonDown(0))
            {
                // I was trying to fix a glitch where you can place things inside of one another but uhh idk so ima do that later.. -nimacs
                /*Vector3 newPos = new Vector3(RoundToGrid(mousePos).x, RoundToGrid(mousePos).y, transform.position.z);
                currentObj.position = Vector3.Lerp(currentObj.position, newPos, 1 * Time.deltaTime);

                while (Mathf.Approximately(newPos.x, currentObj.position.x) && Mathf.Approximately(newPos.y, currentObj.position.y))
                    Debug.Log("Waiting...");*/

                if (!currentObj.gameObject.GetComponent<MaterialScript>().touchingObject())
                {
                    currentObj.position = new Vector3(RoundToGrid(mousePos).x, RoundToGrid(mousePos).y, transform.position.z);
                    currentObj.GetChild(0).gameObject.SetActive(false);
                    currentObj = null;
                }
                else
                    currentObj.gameObject.GetComponent<MaterialScript>().flash();
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0) && grabableObjs.objsList.Count != 0)
                currentObj = grabableObjs.objsList[0];

            if (grabableObjs.objsList.Count != 0)
                grabableObjs.objsList[0].GetChild(0).gameObject.SetActive(true);
        }

        if (Input.GetMouseButtonDown(1) && grabableObjs.objsList.Count != 0)
        {
            Destroy(grabableObjs.objsList[0].gameObject);
            currentObj = null;

            costTEMPTHINGGint -= 1;
        }

        currentObjSO.objsList[0] = currentObj;
    }

    Vector3 RoundToGrid(Vector3 mousePos)
    {
        Vector3 rounded = mousePos;
        bool negX = false,
             negY = false;

        if (rounded.x < 0)
            negX = true;
        if (rounded.y < 0)
            negY = true;

        //float x = Mathf.Round(rounded.x);
        //float y = Mathf.Round(rounded.y);

        float x = rounded.x + gridX / 2;
        x -= x % gridX;
        if (negX)
            x -= gridX;

        float y = rounded.y + gridY / 2;
        y -= y % gridY;
        if (negY)
            y -= gridY;

        rounded = new Vector3(x, y, rounded.z);

        return rounded;
    }
}