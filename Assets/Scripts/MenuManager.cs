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
}
