using UnityEngine;
// You need to call this line so you can call SceneManager
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void GotToLevel()
    {
        SceneManager.LoadScene("BuildingTest");
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