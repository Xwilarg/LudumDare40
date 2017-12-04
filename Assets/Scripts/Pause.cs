using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }

    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void backToGame()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().removePause();
    }

    public void quit()
    {
        Application.Quit();
    }

    public void mainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
