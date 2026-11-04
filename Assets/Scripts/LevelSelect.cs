using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public void GotToLevel(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void GotToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
