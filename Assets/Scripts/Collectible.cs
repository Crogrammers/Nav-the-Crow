using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Collectible : MonoBehaviour
{
    public int coinCount = 25;
    public TextMeshProUGUI coinText;

    void Update()
    {
        coinText.text = "Money: $" + coinCount.ToString();

        if(coinCount == 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    // On trigger enter
    /*
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FindAnyObjectByType<LevelUIController>().collect();
            Destroy(gameObject);
        }
    }
    */
}
