using UnityEngine;
// You need to call this line so you can call SceneManager
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void GotToLevel(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void GotToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}