using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	public void quit()
    {
        Application.Quit();
    }

    public void launchGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void launchGame2()
    {
        SceneManager.LoadScene("MainScene2");
    }

    public void launchGameRandom()
    {
        SceneManager.LoadScene("RandomScene");
    }
}
