using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private string firstLevelName = "Level1";

    public void PlayGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(firstLevelName);
    }

    public void QuitGame()
    {
        Debug.Log("Player has quit the game!");
        Application.Quit();
    }
}
