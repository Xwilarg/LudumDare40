using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadMainGame : MonoBehaviour {
    
    private int currentLevel;
    private CountDeath cd;
    public bool doesRemovePoints;

    private void Start()
    {
        currentLevel = GameObject.FindGameObjectWithTag("DeathManager").GetComponent<CountDeath>().levelPlaying;
        cd = GameObject.FindGameObjectWithTag("DeathManager").GetComponent<CountDeath>();
    }

	public void reload()
    {
        if (doesRemovePoints)
            cd.score -= 10;
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
