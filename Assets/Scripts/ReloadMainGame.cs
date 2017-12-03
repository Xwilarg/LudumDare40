using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadMainGame : MonoBehaviour {

    int currentLevel;

    private void Start()
    {
        currentLevel = GameObject.FindGameObjectWithTag("DeathManager").GetComponent<CountDeath>().levelPlaying;
    }

	public void reload()
    {
        if (currentLevel == 1)
            SceneManager.LoadScene("MainScene");
        else if (currentLevel == 2)
            SceneManager.LoadScene("MainScene2");
        else if (currentLevel == 3)
            SceneManager.LoadScene("RandomScene");
    }

    public void mainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
