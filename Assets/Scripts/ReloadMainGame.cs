﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadMainGame : MonoBehaviour {

	public void reload()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void mainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
