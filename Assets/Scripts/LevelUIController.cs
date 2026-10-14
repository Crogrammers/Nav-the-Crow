using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelUIController : MonoBehaviour
{
    public int lives = 3;
    public int score;
    public float totalTime;
    public TextMeshProUGUI LivesLabel;
    public TextMeshProUGUI ScoreLabel;
    public TextMeshProUGUI TimeLabel;

    void Update()
    {
        totalTime += Time.deltaTime;

        LivesLabel.text = "Lives: " + lives;
        ScoreLabel.text = "Score: " + score;
        TimeLabel.text = "Time: " + totalTime.ToString(format: "0.0###") + "s";
    }
    public void Kill()
    {
        // Kill the player once, if it reaches 0,
        // transition to a game over screen.
        lives--;
        if(lives == 0)
            SceneManager.LoadScene("GameOver");
    }
}
