using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private BoolSO isPaused;

    [SerializeField] private GameObject pauseCanvas;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isPaused.boolean = false;
        pauseCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Input.GetKey(KeyCode.Escape);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused.boolean = !isPaused.boolean;
            pauseCanvas.SetActive(isPaused.boolean);

            /*if (isPaused.boolean)
            {
                isPaused.boolean = false;
                pauseCanvas.SetActive(false);
            }
            else if (!isPaused.boolean)
            {
                isPaused.boolean = true;
            }*/

        }
    }
}
